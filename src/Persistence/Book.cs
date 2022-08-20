namespace Persistence
{
    public class Book
    {
        public int bookId;
        public Category? categoryId;
        public string? bookName;
        public string? authorname;
        public decimal bookPrice;
        public string? bookDescription;
        public int amount;

        public override string ToString()
        {
            return $"{this.bookId} - {this.categoryId} - {this.bookName} - {this.bookPrice} - {this.bookDescription} - {this.authorname}";
        }
    }
}