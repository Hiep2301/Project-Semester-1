using Persistence;
using DAL;

namespace BL
{
    public class StaffBL
    {
        private StaffDAL staffDal = new StaffDAL();

        public Staff Login(Staff staff)
        {
            return staffDal.Login(DbConfig.OpenConnection(), staff);
        }

        public Staff GetStaffById(int id)
        {
            return staffDal.GetStaffById(DbConfig.OpenConnection(), id);
        }

        public List<Staff> GetStaffByName(string name)
        {
            return staffDal.GetStaffByName(DbConfig.OpenConnection(), name);
        }

        public List<Staff> GetAllStaff()
        {
            return staffDal.GetAllStaff(DbConfig.OpenConnection());
        }
    }
}