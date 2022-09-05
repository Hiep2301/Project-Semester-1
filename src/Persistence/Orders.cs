namespace Persistence
{
    public static class OrderStatus
    {
        public const int CREATE_NEW_ORDER = 1;
    }

    public class Orders
    {
        public int orderId { get; set; }
        public Customer? orderCustomer { get; set; }
        public Staff? orderStaff { get; set; }
        public string? staffName { get; set; }
        public string? customerName { get; set; }
        public DateTime orderDate { get; set; }
        public int orderStatus { get; set; }
        public List<Book>? booksList { get; set; }
        public decimal total { get; set; }
        public Book this[int index]
        {
            get
            {
                if (booksList == null || booksList.Count == 0 || index < 0 || booksList.Count < index)
                {
                    return null!;
                };
                return booksList[index];
            }
            set
            {
                if (booksList == null) booksList = new List<Book>();
                booksList.Add(value);
            }
        }

        public Orders()
        {
            booksList = new List<Book>();
        }
    }

    public class Payment
    {
        public decimal paymentAmount { get; set; }
        public decimal refund { get; set; }
    }
}