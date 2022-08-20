using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IOrderDAL
    {
        public Orders CreateOrder(MySqlConnection connection, Orders order);
        public int GetIdOrder(MySqlConnection connection);
        public List<Orders> GetAllOrderInDay(MySqlConnection connection);
        public Orders GetOrderById(MySqlConnection connection, int id);
    }
}