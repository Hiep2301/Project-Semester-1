using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class AdminDAL
    {
        private MySqlConnection connection = DbConfig.OpenConnection();

        public Admin Login(Admin admin)
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
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            _admin = GetAdmin(reader);
                        }
                        reader.Close();
                    }
                }
            }
            catch { }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _admin;
        }

        internal Admin GetAdmin(MySqlDataReader reader)
        {
            Admin admin = new Admin();
            admin.setAdminId(reader.GetInt32("admin_id"));
            admin.setUserName(reader.GetString("user_name"));
            admin.setPassword(reader.GetString("password"));
            return admin;
        }
    }
}