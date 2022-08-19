using MySql.Data.MySqlClient;
using Persistence;


namespace DAL
{
    public interface IAdminDAL
    {
        public Admin Login(MySqlConnection connection, Admin admin);
        public bool InsertBook(MySqlConnection connection, Book book);
        public bool UpdateBookById(MySqlConnection connection, Book book, int id);
        public bool DeleteBookById(MySqlConnection connection, int id);
    }
}