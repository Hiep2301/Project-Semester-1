using Persistence;
using DAL;

namespace BL
{
    public class AdminBL
    {
        private AdminDAL adminDal = new AdminDAL();

        public Admin Login(Admin admin)
        {
            return adminDal.Login(DbConfig.OpenConnection(), admin);
        }

        public bool InsertBook(Book book)
        {
            return adminDal.InsertBook(DbConfig.OpenConnection(), book);
        }

        public bool DeleteBookById(int id)
        {
            return adminDal.DeleteBookById(DbConfig.OpenConnection(), id);
        }
    }
}