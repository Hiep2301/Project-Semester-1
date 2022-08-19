using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class OrderDAL : IOrderDAL
    {
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
            throw new NotImplementedException();
        }

        public List<Order> GetOrderUnpaid(MySqlConnection connection)
        {
            throw new NotImplementedException();
        }

        public bool Payment(Order order)
        {
            throw new NotImplementedException();
        }

        private Order GetOrder(MySqlDataReader reader)
        {
            Order order = new Order();
            // order.ID_E = new Employees();
            // order.ID_Order = reader.GetInt32("ID_Order");
            // order.ID_E.ID_E = reader.GetInt32("ID_E");
            // order.creation_time = reader.GetDateTime("creation_time");
            return order;
        }
    }
}