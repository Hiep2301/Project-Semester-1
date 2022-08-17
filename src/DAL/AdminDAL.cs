using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class AdminDAL
    {
        public Admin Login(MySqlConnection connection, Admin admin)
        {
            MySqlCommand cmd = new MySqlCommand("sp_loginAdmin", connection);
            Admin _admin = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_userName", admin.getUserName());
                cmd.Parameters.AddWithValue("@_password", admin.getPassword());
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _admin = GetAdmin(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _admin;
        }

        private Admin GetAdmin(MySqlDataReader reader)
        {
            Admin admin = new Admin();
            admin.setAdminId(reader.GetInt32("admin_id"));
            admin.setUserName(reader.GetString("user_name"));
            admin.setPassword(reader.GetString("password"));
            admin.setFirstName(reader.GetString("first_name"));
            admin.setLastName(reader.GetString("last_name"));
            admin.setPhone(reader.GetString("phone"));
            return admin;
        }
    }
}