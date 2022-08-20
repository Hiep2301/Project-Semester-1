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

        public bool CreateOrder(Orders order)
        {
            if (order == null || order.booksList == null || order.booksList.Count == 0)
            {
                return false;
            }
            bool result = true;
            MySqlConnection connection = DbConfig.OpenConnection();
            MySqlCommand cmd = connection.CreateCommand();
            cmd.Connection = connection;
            //Lock update all tables
            cmd.CommandText = "lock tables Customers write, Orders write, Items write, OrderDetails write;";
            cmd.ExecuteNonQuery();
            MySqlTransaction trans = connection.BeginTransaction();
            cmd.Transaction = trans;
            MySqlDataReader reader = null!;
            if (order.customerId == null || order.customerId.customerName == null ||
            order.customerId.customerName == "")
            {

                //set default customer with customer id = 1
                order.customerId = new Customer() { customerId = 1 };
            }
            try
            {
                if (order.customerId.customerId == null)
                {
                    cmd.CommandText = @"insert into Customers(customer_name, customer_address)
values ('" + order.customerId.customerName + "','" +

                    (order.customerId.address ?? "") + "');";

                    cmd.ExecuteNonQuery();
                    cmd.CommandText = "select customer_id from Customers order by customer_id desc limit 1;";
                    reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        order.customerId.customerId = reader.GetInt32("customer_id");
                    }
                    reader.Close();
                }
                else
                {
                    order.customerId = (new CustomerDAL()).GetCustomerById(DbConfig.GetConnection(), order.customerId.customerId);
                }
                if (order.customerId == null || order.customerId.customerId == null)
                {
                    throw new Exception("Can't find Customer!");
                }
                cmd.CommandText = "insert into Orders(customer_id, order_status) values (@customerId, @orderStatus); ";

                cmd.Parameters.Clear();

                cmd.Parameters.AddWithValue("@customerId", order.customerId.customerId);
                cmd.Parameters.AddWithValue("@orderStatus", OrderStatus.PROCESSING);
                cmd.ExecuteNonQuery();

                cmd.CommandText = "select LAST_INSERT_ID() as order_id";
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    order.orderId = reader.GetInt32("order_id");
                }
                reader.Close();
                foreach (var item in order.booksList)
                {
                    if (item.book.bookId == null || item.quantity <= 0)
                    {
                        throw new Exception("Not Exists Item");

                    }

                    cmd.CommandText = "select unit_price from Items where item_id=@itemId";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@itemId", item.book.bookId);
                    reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        throw new Exception("Not Exists Item");
                    }
                    cmd.CommandText = "update Items set amount=amount-@quantity where item_id=" + item.book.bookId + ";";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@quantity", item.quantity);
                    cmd.ExecuteNonQuery();
                }
                // commit transaction
                trans.Commit();
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                result = false;
                try
                {
                    trans.Rollback();
                }
                catch
                {
                }
                finally
                {
                    cmd.CommandText = "unlock tables;";
                    cmd.ExecuteNonQuery();
                    DbConfig.CloseConnection();
                }
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

        public List<Orders> GetOrderUnpaid(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public bool Payment(Orders order)
        {
            throw new NotImplementedException();
        }

        public List<Orders> GetAllOrderInDay(MySqlConnection connection)
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
            query = $@"select * from Orders where day(Creation_Time) + month(Creation_Time) + year(Creation_Time) = '{DateTime.Now.Day}' + '{DateTime.Now.Month}' + '{DateTime.Now.Year}';";
            reader = (new MySqlCommand(query, connection)).ExecuteReader();
            List<Orders> orderslist = new List<Orders>();
            Orders order = null!;
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
            reader.Close();
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