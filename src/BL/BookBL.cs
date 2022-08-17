using Persistence;
using DAL;

namespace BL
{
    public class BookBL
    {
        private BookDAL bookDal = new BookDAL();

        public Book GetBookById(int id)
        {
            return bookDal.GetBookById(DbConfig.OpenConnection(), id);
        }

        public List<Book> GetBookByName(string name)
        {
            return bookDal.GetBookByName(DbConfig.OpenConnection(), name);
        }

        public List<Book> GetAllBook()
        {
            return bookDal.GetAllBook(DbConfig.OpenConnection());
        }
    }
}