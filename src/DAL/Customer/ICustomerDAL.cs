using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface ICustomerDAL
    {
        public Customer Login(MySqlConnection connection, Customer customer);
        public Customer GetCustomerById(MySqlConnection connection, int id);
        public List<Customer> GetCustomerByName(MySqlConnection connection, string name);
        public List<Customer> GetAllCustomer(MySqlConnection connection);

    }
}