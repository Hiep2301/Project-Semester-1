using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CustomerDAL
    {
        public Customer Login(MySqlConnection connection, Customer customer)
        {
            MySqlCommand cmd = new MySqlCommand("sp_loginCustomer", connection);
            Customer _customer = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_userName", customer.getUserName());
                cmd.Parameters.AddWithValue("@_password", customer.getPassword());
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _customer = GetCustomer(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _customer;
        }

        private Customer GetCustomer(MySqlDataReader reader)
        {
            Customer customer = new Customer();
            customer.setCustomerId(reader.GetInt32("customer_id"));
            customer.setUserName(reader.GetString("user_name"));
            customer.setPassword(reader.GetString("password"));
            customer.setCustomerName(reader.GetString("customer_name"));
            customer.setPhone(reader.GetString("customer_phone"));
            customer.setAddress(reader.GetString("customer_address"));
            return customer;
        }

        public Customer GetCustomerById(MySqlConnection connection, int id)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getCustomerById", connection);
            Customer _customer = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_customerId", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _customer = GetCustomer(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _customer;
        }
    }
}