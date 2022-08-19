namespace Persistence
{
    public class Book
    {
        private int bookId;
        private int categoryId;
        private string? bookName;
        private decimal bookPrice;
        private string? bookDescription;
        private string? authorname;

        public Book()
        {

        }

        public Book(int bookId, int categoryId, string bookName, decimal bookPrice, string bookDescription, string authorname)
        {
            this.bookId = bookId;
            this.categoryId = categoryId;
            this.bookName = bookName;
            this.bookPrice = bookPrice;
            this.bookDescription = bookDescription;
            this.authorname = authorname;
        }

        public int? getBookId()
        {
            return this.bookId;
        }

        public void setBookId(int bookId)
        {
            this.bookId = bookId;
        }

        public int? getCategoryId()
        {
            return this.categoryId;
        }

        public void setCategoryId(int categoryId)
        {
            this.categoryId = categoryId;
        }

        public string? getBookName()
        {
            return this.bookName;
        }

        public void setBookName(string bookName)
        {
            this.bookName = bookName;
        }

        public decimal getBookPrice()
        {
            return this.bookPrice;
        }

        public void setBookPrice(decimal bookPrice)
        {
            this.bookPrice = bookPrice;
        }

        public string? getBookDescription()
        {
            return this.bookDescription;
        }

        public void setBookDescription(string bookDescription)
        {
            this.bookDescription = bookDescription;
        }

        public string? getAuthorname()
        {
            return this.authorname;
        }

        public void setAuthorname(string authorname)
        {
            this.authorname = authorname;
        }

        public override string ToString()
        {
            return $"{this.getBookId()} - {this.getCategoryId()} - {this.getBookName()} - {this.getBookPrice()} - {this.getBookDescription()} - {this.getAuthorname()}";
        }
    }
}