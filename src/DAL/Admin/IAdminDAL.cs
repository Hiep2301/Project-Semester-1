using MySql.Data.MySqlClient;
using Persistence;


namespace DAL
{
    public interface IAdminDAL
    {
        public Admin Login(MySqlConnection connection, Admin admin);
    }
}