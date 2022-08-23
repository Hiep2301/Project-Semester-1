using MySql.Data.MySqlClient;
using Persistence;

namespace DAL
{
    public class BookDAL : IBookDAL
    {
        private MySqlConnection connection = DbConfig.GetConnection();
        public Book GetBookById(string searchKeyWord, Book book)
        {
            try
            {
                connection.Open();
                MySqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = $"select book.book_id, book.book_name, book.author_name, book.book_price, book.book_description, book.book_quantity, category.category_name from book inner join category on book.category_id = category.category_id where book.book_id = '{searchKeyWord}';";
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    book.bookId = reader.GetInt32("book_id");
                    book.bookName = reader.GetString("book_name");
                    book.authorName = reader.GetString("author_name");
                    book.bookPrice = reader.GetDecimal("book_price");
                    book.bookDescription = reader.GetString("book_description");
                    book.bookQuantity = reader.GetInt32("book_quantity");
                    book.bookCategory = reader.GetString("category_name");
                }
                else
                {
                    book.bookId = -1;
                }
                reader.Close();
            }
            catch
            {
                book.bookId = -1;
            }
            finally
            {
                connection.Close();
            }
            return book;
        }

        public List<Book> GetBook(List<Book> list, string commandText)
        {
            lock (connection)
            {
                try
                {
                    connection.Open();
                    MySqlCommand cmd = connection.CreateCommand();
                    cmd.CommandText = commandText;
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.bookId = reader.GetInt32("book_id");
                        book.bookName = reader.GetString("book_name");
                        book.authorName = reader.GetString("author_name");
                        book.bookPrice = reader.GetDecimal("book_price");
                        book.bookDescription = reader.GetString("book_description");
                        book.bookQuantity = reader.GetInt32("book_quantity");
                        book.bookCategory = reader.GetString("category_name");
                        list.Add(book);
                    }
                    reader.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Disconnected database");
                }
                finally
                {
                    connection.Close();
                }
                return list;
            }
        }
    }
}