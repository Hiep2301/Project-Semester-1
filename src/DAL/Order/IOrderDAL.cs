using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IOrderDAL
    {
        public bool CreateOrder(MySqlConnection connection, Orders order);
        public List<Orders> GetAllOrderInDay(MySqlConnection connection);
        public Orders GetOrderById(MySqlConnection connection, int id);
    }
}