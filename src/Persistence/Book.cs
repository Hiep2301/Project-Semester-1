namespace Persistence
{
    public class Book
    {
        public int bookId { get; set; }
        public string? bookCategory { get; set; }
        public string? bookName { get; set; }
        public string? authorName { get; set; }
        public decimal bookPrice { get; set; }
        public string? bookDescription { get; set; }
        public int bookQuantity { get; set; }
        public double bookAmount { get; set; }
    }
}