using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface IBookDAL
    {
        public Book GetBookById(MySqlConnection connection, string searchKeyWord);
        public List<Category> GetAllCategory(MySqlConnection connection);
    }
}