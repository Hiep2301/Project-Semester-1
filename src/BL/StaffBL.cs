using Persistence;
using DAL;

namespace BL
{
    public class StaffBL
    {
        private StaffDAL staffDal = new StaffDAL();

        public Staff Login(Staff staff)
        {
            return staffDal.Login(staff);
        }

        public Staff GetStaffById(int id)
        {
            return staffDal.GetStaffById(id);
        }

        public List<Staff> GetStaffByName(string name)
        {
            return staffDal.GetStaffByName(name);
        }

        public List<Staff> GetAllStaff()
        {
            return staffDal.GetAllStaff();
        }
    }
}