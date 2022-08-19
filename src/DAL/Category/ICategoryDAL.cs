using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public interface ICategoryDAL
    {
        public Category GetCategoryById(MySqlConnection connection, int id);
        public List<Category> GetCategoryByName(MySqlConnection connection, string name);
        public List<Category> GetAllCategory(MySqlConnection connection);
    }
}