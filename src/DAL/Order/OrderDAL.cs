using MySql.Data.MySqlClient;
using Persistence;
using System.Text.RegularExpressions;

namespace DAL
{
    public class OrderDAL : IOrderDAL
    {
        private MySqlDataReader? reader;
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
                    while (true)
                    {
                        if (Regex.Match(customerPhone, @"^(\+[0-9])$").Success)
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Số điện thoại không hợp lệ!");
                            Console.Write("Nhập số điện thoại khách hàng: ");
                            customerPhone = Console.ReadLine() ?? "";
                        }

                    }
                    cmd.CommandText = $"select * from customer where customer_phone = {customerPhone};";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Console.WriteLine($"Khách hàng cũ: {reader.GetString("customer_name")}");
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
                        order.orderCustomer = new Customer { customerName = customerName, customerPhone = customerPhone };
                        cmd.CommandText = $"insert into customer(customer_name, customer_phone) values ('{order.orderCustomer.customerName}', '{order.orderCustomer.customerPhone}');";
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
                catch
                {
                    Console.WriteLine("Disconnected database");
                    try
                    {
                        trans.Rollback();
                    }
                    catch
                    {
                        Console.WriteLine("Disconnected database");
                    }
                }
                finally
                {
                    cmd.CommandText = "unlock tables;";
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
                Console.WriteLine("Disconnected database");
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public List<Orders> GetAllOrderInDay()
        {
            List<Orders> list = new List<Orders>();
            Orders order = null!;
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select orders.order_id, orders.order_date, order_details.unit_price, staff.staff_name, customer.customer_name from orders inner join order_details on orders.order_id = order_details.order_id inner join staff on orders.staff_id = staff.staff_id inner join customer on orders.customer_id = customer.customer_id where day(order_date) + month(order_date) + year(order_date) = '{DateTime.Now.Day}' + '{DateTime.Now.Month}' + '{DateTime.Now.Year}';";
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
            catch
            {
                Console.WriteLine("Disconnected database");
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
            order.staffName = reader.GetString("staff_name");
            order.customerName = reader.GetString("customer_name");
            order.orderId = reader.GetInt32("order_id");
            order.orderDate = reader.GetDateTime("order_date");
            order.total = reader.GetDecimal("unit_price");
            return order;
        }

        private bool IsContinue(string text)
        {
            string input;
            bool isMatch;
            Console.Write(text);
            input = Console.ReadLine() ?? "";
            isMatch = Regex.IsMatch(input, @"^[yYnN]$");
            while (true)
            {
                if (Regex.Match(input, @"^[yYnN]$").Success)
                {
                    break;
                }
                else
                {
                    Console.Write("Chọn (Y/N): ");
                    input = Console.ReadLine() ?? "";
                }
            }
            if (input == "y" || input == "Y")
            {
                return true;
            }
            return false;
        }
    }
}