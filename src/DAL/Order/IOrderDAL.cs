using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IOrderDAL
    {
        public bool CreateOrder(Order order);
        public bool Payment(Order order);
        public bool ChangeStatus(int status, int id);
        public Order GetOrderById(MySqlConnection connection, int id);
        public List<Order> GetOrderUnpaid(MySqlConnection connection);

    }
}