using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IStaffDAL
    {
        public Staff Login(Staff staff);
        public Staff GetStaffById(int id);
        public List<Staff> GetStaffByName(string name);
        public List<Staff> GetAllStaff();
    }
}