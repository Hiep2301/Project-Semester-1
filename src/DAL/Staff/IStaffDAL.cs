using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IStaffDAL
    {
        public Staff Login(MySqlConnection connection, Staff staff);
        public Staff GetStaffById(MySqlConnection connection, int id);
        public List<Staff> GetStaffByName(MySqlConnection connection, string name);
        public List<Staff> GetAllStaff(MySqlConnection connection);
    }
}