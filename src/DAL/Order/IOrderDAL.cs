using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IOrderDAL
    {
        public bool CreateOrder(Orders order);
        public bool Payment(Orders order);
        public bool ChangeStatus(int status, int id);
        public List<Orders> GetAllOrderInDay(MySqlConnection connection);
        public Orders GetOrderById(MySqlConnection connection, int id);
        public List<Orders> GetOrderUnpaid(MySqlConnection connection);

    }
}