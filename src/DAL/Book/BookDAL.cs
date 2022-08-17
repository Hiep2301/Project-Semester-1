using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class BookDAL : IBookDAL
    {
        public Book GetBookById(MySqlConnection connection, int id)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getBookById", connection);
            Book _book = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_bookId", id);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        _book = GetBook(reader);
                    }
                    reader.Close();
                }
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return _book;
        }

        public List<Book> GetBookByName(MySqlConnection connection, string name)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getBookByName", connection);
            List<Book> list = null!;
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_bookName", name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    list = new List<Book>();
                    while (reader.Read())
                    {
                        list.Add(GetBook(reader));
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

        public List<Book> GetAllBook(MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand("sp_getAllBook", connection);
            List<Book> list = null!;
            using (MySqlDataReader reader = cmd.ExecuteReader())
            {
                try
                {
                    list = new List<Book>();
                    while (reader.Read())
                    {
                        list.Add(GetBook(reader));
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

        private Book GetBook(MySqlDataReader reader)
        {
            Book book = new Book();
            book.setBookId(reader.GetInt32("book_id"));
            book.setCategoryId(reader.GetInt32("category_id"));
            book.setBookName(reader.GetString("book_name"));
            book.setBookPrice(reader.GetDecimal("book_price"));
            book.setBookDescription(reader.GetString("book_description"));
            return book;
        }
    }
}