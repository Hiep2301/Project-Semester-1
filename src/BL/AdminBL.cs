using Persistence;
using DAL;

namespace BL
{
    public class AdminBL
    {
        private AdminDAL adminDal = new AdminDAL();

        public Admin Login(Admin admin)
        {
            return adminDal.Login(admin);
        }

    }
}