using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class StaffDAL
    {
        public Staff Login(MySqlConnection connection, Staff staff)
        {
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select * from staff where staff_username = '{staff.userName}' and staff_password = '{Md5Algorithms.CreateMD5(staff.password!)}';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        staff = GetStaff(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return staff;
        }

        public Staff GetStaffById(MySqlConnection connection, int id)
        {
            Staff staff = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select * from staff where staff_id = {id};";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        staff = GetStaff(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return staff;
        }

        public List<Staff> GetStaffByName(MySqlConnection connection, string name)
        {
            List<Staff> list = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select * from staff where staff_name like concat('%', {name}, '%');";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    list = new List<Staff>();
                    while (reader.Read())
                    {
                        list.Add(GetStaff(reader));
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

        public List<Staff> GetAllStaff(MySqlConnection connection)
        {
            List<Staff> list = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select * from staff;";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    list = new List<Staff>();
                    while (reader.Read())
                    {
                        list.Add(GetStaff(reader));
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

        private Staff GetStaff(MySqlDataReader reader)
        {
            Staff staff = new Staff();
            staff.staffId = reader.GetInt32("staff_id");
            staff.userName = reader.GetString("staff_username");
            staff.password = reader.GetString("staff_password");
            staff.staffName = reader.GetString("staff_name");
            staff.staffPhone = reader.GetString("staff_phone");
            staff.staffAddress = reader.GetString("staff_address");
            staff.staffRole = reader.GetInt32("staff_role");
            return staff;
        }
    }
}