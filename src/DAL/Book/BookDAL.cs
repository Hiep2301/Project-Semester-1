using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class BookDAL : IBookDAL
    {
        public Book GetBookById(MySqlConnection connection, string searchKeyWord)
        {
            Book book = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"SELECT book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name FROM book INNER JOIN category ON book.category_id = category.category_id WHERE book.book_id = '{searchKeyWord}';";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        book = GetBook(reader);
                    }
                    else
                    {
                        book.bookId = -1;
                    }
                    reader.Close();
                }
            }
            catch
            {
                book.bookId = -1;
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return book;
        }

        public List<Category> GetAllCategory(MySqlConnection connection)
        {
            List<Category> list = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = $"select * from category;";
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    list = new List<Category>();
                    while (reader.Read())
                    {
                        Category category = new Category();
                        category.categoryId = reader.GetInt32("category_id");
                        category.categoryName = reader.GetString("category_name");
                        list.Add(category);
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

        private Book GetBook(MySqlDataReader reader)
        {
            Book book = new Book();
            book.bookId = reader.GetInt32("book_id");
            book.bookCategory!.categoryId = reader.GetInt32("category_id");
            book.bookName = reader.GetString("book_name");
            book.authorName = reader.GetString("author_name");
            book.bookPrice = reader.GetDecimal("book_price");
            book.bookDescription = reader.GetString("book_description");
            book.bookQuantity = reader.GetInt32("book_quantity");
            return book;
        }

        public List<Book> GetBooks(MySqlConnection connection, List<Book> list, string commandText)
        {
            Book book = null!;
            try
            {
                MySqlCommand cmd = connection.CreateCommand();
                cmd.Connection = connection;
                cmd.CommandText = commandText;
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        book.bookId = reader.GetInt32("book_id");
                        book.bookName = reader.GetString("book_name");
                        book.authorName = reader.GetString("author_name");
                        book.bookPrice = reader.GetDecimal("book_price");
                        book.bookCategory!.categoryName = reader.GetString("category_name");
                        book.bookDescription = reader.GetString("book_description");
                        book.bookQuantity = reader.GetInt32("book_quantity");
                        list.Add(book);
                    }
                    reader.Close();
                }
            }
            catch
            {
                Console.WriteLine("Lost Database Connection");
            }
            finally
            {
                DbConfig.CloseConnection();
            }
            return list;
        }
    }
}