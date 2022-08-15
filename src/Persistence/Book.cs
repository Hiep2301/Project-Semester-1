namespace Persistence
{
    public class Book
    {
        private int bookId;
        private int categoryId;
        private string? bookName;
        private decimal bookPrice;
        private string? bookDescription;

        public Book()
        {

        }

        public Book(int bookId, int categoryId, string bookName, decimal bookPrice, string bookDescription)
        {
            this.bookId = bookId;
            this.categoryId = categoryId;
            this.bookName = bookName;
            this.bookPrice = bookPrice;
            this.bookDescription = bookDescription;
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
    }
}