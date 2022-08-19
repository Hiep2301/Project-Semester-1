using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class CategoryDAL : ICategoryDAL
    {
       public Category GetCategoryById(MySqlConnection connection, int id)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getCategoryById", connection);
            Category _category = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_categoryId", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _category = GetCategory(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _category;
        }

        public List<Category> GetCategoryByName(MySqlConnection connection, string name)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getCategoryByName", connection);
            List<Category> list = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_categoryName", name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    list = new List<Category>();
                    while (reader.Read())
                    {
                        list.Add(GetCategory(reader));
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return list;
        }

        public List<Category> GetAllCategory(MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getAllCategory", connection);
            List<Category> list = null!;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                try
                {
                    list = new List<Category>();
                    while (reader.Read())
                    {
                        list.Add(GetCategory(reader));
                    }
                    reader.Close();
                }
                finally
                {
                    DbConfig.CloseConnection();
                }
            }
            return list;
        }

        private Category GetCategory(MySqlDataReader reader)
        {
            Category category = new Category();
            category.setCategoryId(reader.GetInt32("category_id"));
            category.setCategoryName(reader.GetString("category_name"));
            return category;
        }
    }
}