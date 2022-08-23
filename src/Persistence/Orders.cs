namespace Persistence
{
    public static class OrderStatus
    {
        public const int CREATE_NEW_ORDER = 1;
    }

    public class Orders
    {
        public int orderId;
        public Customer? orderCustomer;
        public Staff? orderStaff;
        public DateTime orderDate;
        public int orderStatus;
        public List<Book>? booksList;
        public double total;
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
        public decimal paymentAmount;
        public decimal refund;
    }
}