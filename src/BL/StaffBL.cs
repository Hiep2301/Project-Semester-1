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
    }
}