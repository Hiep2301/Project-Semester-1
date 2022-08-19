using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CustomerDAL : ICustomerDAL
    {
        public Customer Login(MySqlConnection connection, Customer customer)
        {
            MySqlCommand cmd = new MySqlCommand("sp_loginCustomer", connection);
            Customer _customer = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_userName", customer.userName);
                cmd.Parameters.AddWithValue("@_password", customer.password);
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
            customer.customerId = reader.GetInt32("customer_id");
            customer.userName = reader.GetString("user_name");
            customer.password = reader.GetString("password");
            customer.customerName = reader.GetString("customer_name");
            customer.phone = reader.GetString("customer_phone");
            customer.address = reader.GetString("customer_address");
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

        public List<Customer> GetCustomerByName(MySqlConnection connection, string name)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getCustomerByName", connection);
            List<Customer> list = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_customerName", name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    list = new List<Customer>();
                    while (reader.Read())
                    {
                        list.Add(GetCustomer(reader));
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return list;
        }

        public List<Customer> GetAllCustomer(MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getAllCustomer", connection);
            List<Customer> list = null!;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                try
                {
                    list = new List<Customer>();
                    while (reader.Read())
                    {
                        list.Add(GetCustomer(reader));
                    }
                    reader.Close();
                }
                finally
                {
                    DbConfig.CloseConnection();
                }
            }
            return list;
        }
    }
}