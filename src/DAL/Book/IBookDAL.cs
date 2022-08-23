using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IBookDAL
    {
        public Book GetBookById(string searchKeyWord, Book book);
    }
}