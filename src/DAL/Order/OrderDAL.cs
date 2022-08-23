using MySql.Data.MySqlClient;
using Persistence;
using System.Text.RegularExpressions;

namespace DAL
{
    public class OrderDAL : IOrderDAL
    {
        public MySqlDataReader? reader;
        private MySqlConnection connection = DbConfig.GetConnection();
        public bool CreateOrder(Orders order)
        {
            if (order == null || order.booksList == null || order.booksList.Count == 0)
            {
                return false;
            }
            bool result = false;
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                //Khoá cập nhật tất cả table, bảo đảm tính toàn vẹn dữ liệu
                cmd.CommandText = "lock tables staff write, book write, category write, customer write, order_details write, orders write;";
                cmd.ExecuteNonQuery();
                MySqlTransaction trans = connection.BeginTransaction();
                cmd.Transaction = trans;
                bool check = false;
                if (order.orderCustomer == null || order.orderCustomer.customerName == null || order.orderCustomer.customerName == "")
                {
                    order.orderCustomer = new Customer() { customerId = 1 };
                }
                try
                {
                    Console.Write("Nhập số điện thoại khách hàng: ");
                    string customerPhone = Console.ReadLine() ?? "";
                    while (!(Regex.IsMatch(customerPhone, @"^(0|\+84)\d{9}$")))
                    {
                        Console.WriteLine("Số điện thoại không hợp lệ!");
                        Console.Write("Nhập số điện thoại khách hàng: ");
                        customerPhone = Console.ReadLine() ?? "";
                    }

                    cmd.CommandText = $"select * from customer where customer_phone = {customerPhone};";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine("Người mua là khách hàng cũ!");
                        Console.WriteLine($"Tên khách hàng: {reader.GetString("customer_name")}");
                        order.orderCustomer.customerPhone = reader.GetString("customer_phone");
                        order.orderCustomer.customerId = reader.GetInt32("customer_id");
                        order.orderCustomer.customerName = reader.GetString("customer_name");
                        check = true;
                    }
                    reader.Close();

                    if (!check)
                    {
                        Console.Write("Nhập tên khách hàng: ");
                        string customerName = Console.ReadLine() ?? "";
                        while (!(Regex.IsMatch(customerName, @"(^[A-Z,a-z]+$)|^([A-Z,a-z]+ *)+[A-Z,a-z]$")))
                        {
                            Console.WriteLine("Tên khách hàng không hợp lệ!");
                            Console.Write("Nhập tên khách hàng: ");
                            customerName = Console.ReadLine() ?? "";
                        }
                        order.orderCustomer = new Customer { customerName = customerName, customerPhone = customerPhone };
                        cmd.CommandText = $"insert into customer(customer_name, customer_phone) values ({order.orderCustomer.customerName}, {order.orderCustomer.customerPhone});";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "select LAST_INSERT_ID() as customer_id;";
                        reader = cmd.ExecuteReader();
                        if (reader.Read())
                        {
                            order.orderCustomer.customerId = reader.GetInt32("customer_id");
                        }
                        reader.Close();
                    }
                    cmd.CommandText = $"insert into orders(customer_id, staff_id, order_status) values ({order.orderCustomer!.customerId}, {order.orderStaff!.staffId}, {OrderStatus.CREATE_NEW_ORDER});";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "select LAST_INSERT_ID() as order_id;";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.orderId = reader.GetInt32("order_id");
                    }
                    reader.Close();

                    cmd.CommandText = "SELECT order_date FROM orders ORDER BY order_id DESC LIMIT 1;";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.orderDate = reader.GetDateTime("order_date");
                    }
                    reader.Close();

                    foreach (Book book in order.booksList)
                    {
                        cmd.CommandText = $"select book_price from book where book_id={book.bookId};";
                        reader = cmd.ExecuteReader();
                        if (!reader.Read())
                        {
                            throw new Exception("Không tồn tại");
                        }
                        book.bookPrice = reader.GetDecimal("book_price");
                        reader.Close();

                        cmd.CommandText = $"insert into order_details(order_id, book_id, unit_price, quantity) values ({order.orderId}, {book.bookId}, {book.bookPrice * book.bookQuantity}, {book.bookQuantity});";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = $"update book set book_quantity=book_quantity-{book.bookQuantity} where book_id={book.bookId};";
                        cmd.ExecuteNonQuery();
                    }
                    trans.Commit();
                    result = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    try
                    {
                        trans.Rollback();
                    }
                    catch { }
                }
                finally
                {
                    cmd.CommandText = "unlock tables;";
                    cmd.ExecuteNonQuery();
                }
            }
            catch { }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public Orders GetOrderById(int id)
        {
            Orders order = null!;
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select * from orders where order_id = {id};";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        order = GetOrder(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            return order;
        }

        public List<Orders> GetAllOrderInDay()
        {
            List<Orders> list = new List<Orders>();
            Orders order = null!;
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select * from orders where day(order_date) + month(order_date) + year(order_date) = '{DateTime.Now.Day}' + '{DateTime.Now.Month}' + '{DateTime.Now.Year}';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order = new Orders();
                        order = GetOrder(reader);
                        list.Add(order);
                    }
                    if (list == null || list.Count == 0)
                    {
                        reader.Close();
                        return null!;
                    }
                }
            }
            finally
            {
                connection.Close();
            }
            return list;
        }

        private Orders GetOrder(MySqlDataReader reader)
        {
            Orders order = new Orders();
            order.orderCustomer = new Customer();
            order.orderId = reader.GetInt32("order_id");
            order.orderCustomer.customerId = reader.GetInt32("customer_id");
            order.orderStaff!.staffId = reader.GetInt32("staff_id");
            order.orderDate = reader.GetDateTime("order_date");
            return order;
        }
    }
}