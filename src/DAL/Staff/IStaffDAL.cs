using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IStaffDAL
    {
        public Staff Login(Staff staff);
    }
}