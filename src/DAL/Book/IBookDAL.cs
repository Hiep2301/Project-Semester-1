using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IBookDAL
    {
        public Book GetBookById(MySqlConnection connection, int id);
        public List<Book> GetBookByName(MySqlConnection connection, string name);
        public List<Book> GetAllBook(MySqlConnection connection);
    }
}