using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class StaffDAL : IStaffDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public Staff Login(Staff staff)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
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
                connection.Close();
            }
            return staff;
        }

        public Staff GetStaffById(int id)
        {
            Staff staff = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                connection.Open();
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
                connection.Close();
            }
            return staff;
        }

        public List<Staff> GetStaffByName(string name)
        {
            List<Staff> list = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                connection.Open();
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
                connection.Close();
            }
            return list;
        }

        public List<Staff> GetAllStaff()
        {
            List<Staff> list = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                connection.Open();
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
                connection.Close();
            }
            return list;
        }

        private Staff GetStaff(MySqlDataReader reader)
        {
            Staff staff = new Staff();
            staff.staffId = reader.GetInt32("staff_id");
            staff.staffName = reader.GetString("staff_name");
            staff.staffPhone = reader.GetString("staff_phone");
            staff.staffAddress = reader.GetString("staff_address");
            staff.staffRole = reader.GetInt32("staff_role");
            return staff;
        }
    }
}