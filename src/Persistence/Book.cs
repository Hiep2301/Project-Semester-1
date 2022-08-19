namespace Persistence
{
    public class Book
    {
        public int bookId;
        public Category? categoryId;
        public string? bookName;
        public decimal bookPrice;
        public string? bookDescription;
        public string? authorname;

        public Book()
        {

        }

        public Book(int bookId, Category categoryId, string bookName, decimal bookPrice, string bookDescription, string authorname)
        {
            this.bookId = bookId;
            this.categoryId = categoryId;
            this.bookName = bookName;
            this.bookPrice = bookPrice;
            this.bookDescription = bookDescription;
            this.authorname = authorname;
        }

        public override string ToString()
        {
            return $"{this.bookId} - {this.categoryId} - {this.bookName} - {this.bookPrice} - {this.bookDescription} - {this.authorname}";
        }
    }
}