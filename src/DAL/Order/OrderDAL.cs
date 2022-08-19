using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class OrderDAL : IOrderDAL
    {
        private string? query;
        public MySqlDataReader? reader;

        public bool ChangeStatus(int status, int id)
        {
            throw new NotImplementedException();
        }

        public bool CreateOrder(Order order)
        {

            return true;
        }

        public Order GetOrderById(MySqlConnection connection, int id)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getOrderById", connection);
            Order _order = null!;
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

        public List<Order> GetOrderUnpaid(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public bool Payment(Order order)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAllOrderInDay(MySqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = $@"select * from Orders where day(Creation_Time) + month(Creation_Time) + year(Creation_Time) = '{DateTime.Now.Day}' + '{DateTime.Now.Month}' + '{DateTime.Now.Year}';";
            reader = (new MySqlCommand(query, connection)).ExecuteReader();
            List<Order> orderslist = new List<Order>();
            Order order = null!;
            while (reader.Read())
            {
                order = new Order();
                order = GetOrder0(reader);
                orderslist.Add(order);
            }
            if (orderslist == null || orderslist.Count == 0)
            {
                reader.Close();
                return null!;
            }
            reader.Close();
            return orderslist;
        }

        private Order GetOrder0(MySqlDataReader reader)
        {
            Order order = new Order();
            order.customerId = new Customer();
            order.orderId = reader.GetInt32("order_id");
            order.customerId.customerId = reader.GetInt32("customer_id");
            order.orderDate = reader.GetDateTime("order_date");
            return order;
        }

        private Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
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