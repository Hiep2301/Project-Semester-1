using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class OrderDAL : IOrderDAL
    {
        private string? query;
        public MySqlDataReader? reader;

        public Orders CreateOrder(MySqlConnection connection, Orders order)
        {
            if (order == null || order.booksList == null || order.booksList.Count == 0)
            {
                return null!;
            }
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            //Khoá cập nhật tất cả table, bảo đảm tính toàn vẹn dữ liệu
            cmd.CommandText = "lock tables admin write, book write, category write, customer write, order_details write, orders write;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            try
            {
                // Nhập dữ liệu cho bảng Order
                cmd.CommandText = $"insert into orders(customer_id) value ({order.customerId!.customerId});";
                cmd.ExecuteNonQuery();
                int ID_Order = GetIdOrder(DbConfig.OpenConnection()) + 1;
                //Nhập dữ liệu cho bảng OrderDetail
                for (int i = 0; i < order.booksList.Count; i++)
                {
                    cmd.CommandText = $@"insert into order_details(order_id,book_id,unit_price,quantity) values
                    ({ID_Order},
                    {order.booksList[i].book.bookId},
                    {order.booksList[i].quantity * order.booksList[i].book.bookPrice},
                    {order.booksList[i].quantity})";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = $"update book set amount = amount - {order.booksList[i].quantity} where ID_Book = {order.booksList[i].book.bookId};";
                    cmd.ExecuteNonQuery();
                }
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                return null!;
            }
            finally
            {
                cmd.CommandText = "unlock tables;";
                cmd.ExecuteNonQuery();
                DbConfig.CloseConnection();
            }

            return order;
        }

        public int GetIdOrder(MySqlConnection connection)
        {
            int result = 0;
            string query = "select order_id from orders order by order_id desc limit 1;";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result = reader.GetInt32("order_id");
                    }
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return result;
        }

        public Orders GetOrderById(MySqlConnection connection, int id)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getOrderById", connection);
            Orders _order = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_orderId", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _order = GetOrder(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _order;
        }

        public List<Orders> GetAllOrderInDay(MySqlConnection connection)
        {
            query = $@"select * from orders where day(order_date) + month(order_date) + year(order_date) = '{DateTime.Now.Day}' + '{DateTime.Now.Month}' + '{DateTime.Now.Year}';";
            MySqlCommand cmd = new MySqlCommand(query, connection);
            List<Orders> orderslist = new List<Orders>();
            Orders order = null!;
            try
            {
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        order = new Orders();
                        order = GetOrder0(reader);
                        orderslist.Add(order);
                    }
                    if (orderslist == null || orderslist.Count == 0)
                    {
                        reader.Close();
                        return null!;
                    }
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return orderslist;
        }

        private Orders GetOrder0(MySqlDataReader reader)
        {
            Orders order = new Orders();
            order.customerId = new Customer();
            order.orderId = reader.GetInt32("order_id");
            order.customerId.customerId = reader.GetInt32("customer_id");
            order.orderDate = reader.GetDateTime("order_date");
            return order;
        }

        private Orders GetOrder(MySqlDataReader reader)
        {
            Orders order = new Orders();
            OrderDetails orderDetails = new OrderDetails();
            order.customerId = new Customer();
            order.booksList = new List<OrderDetails>();

            order.orderId = reader.GetInt32("order_id");
            order.customerId.customerId = reader.GetInt32("customer_id");
            order.orderDate = reader.GetDateTime("order_date");

            orderDetails.book.bookId = reader.GetInt32("book_id");
            orderDetails.book.bookPrice = reader.GetDecimal("book_price");
            orderDetails.quantity = reader.GetInt32("quantity");
            order.booksList.Add(orderDetails);
            return order;
        }
    }
}