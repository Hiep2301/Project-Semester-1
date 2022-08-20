using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class AdminDAL : IAdminDAL
    {
        public Admin Login(MySqlConnection connection, Admin admin)
        {
            MySqlCommand cmd = new MySqlCommand("sp_loginAdmin", connection);
            Admin _admin = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_userName", admin.userName);
                cmd.Parameters.AddWithValue("@_password", Md5Algorithms.CreateMD5(admin.password!));
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _admin = GetAdmin(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _admin;
        }

        public bool InsertBook(MySqlConnection connection, Book book)
        {
            int result = 1;
            MySqlCommand cmd = new MySqlCommand("sp_insertBook", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_bookId", book.bookId);
                cmd.Parameters.AddWithValue("@_categoryId", book.categoryId!.categoryId);
                cmd.Parameters.AddWithValue("@_bookName", book.bookName);
                cmd.Parameters.AddWithValue("@_bookPrice", book.bookPrice);
                cmd.Parameters.AddWithValue("@_bookDescription", book.bookDescription);
                cmd.Parameters.AddWithValue("@_authorName", book.authorname);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            if (result == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteBookById(MySqlConnection connection, int id)
        {
            int result = 1;
            MySqlCommand cmd = new MySqlCommand("sp_deletetBook", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_bookId", id);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public bool UpdateBookById(MySqlConnection connection, Book book, int id)
        {
            int result = 1;
            MySqlCommand cmd = new MySqlCommand("sp_updateBook", connection);
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_bookId", id);
                cmd.Parameters.AddWithValue("@_categoryId", book.categoryId!.categoryId);
                cmd.Parameters.AddWithValue("@_bookName", book.bookName);
                cmd.Parameters.AddWithValue("@_bookPrice", book.bookPrice);
                cmd.Parameters.AddWithValue("@_bookDescription", book.bookDescription);
                cmd.Parameters.AddWithValue("@_authorName", book.authorname);
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = 0;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        private Admin GetAdmin(MySqlDataReader reader)
        {
            Admin admin = new Admin();
            admin.adminId = reader.GetInt32("admin_id");
            admin.userName = reader.GetString("user_name");
            admin.password = reader.GetString("password");
            admin.admintName = reader.GetString("admin_name");
            admin.phone = reader.GetString("phone");
            return admin;
        }
    }
}