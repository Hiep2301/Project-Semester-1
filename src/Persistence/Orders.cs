namespace Persistence
{
    public class OrderDetails
    {
        public Book book = new Book();
        public int quantity;
    }

    public static class OrderStatus
    {
        public const int UNPAID = 1;
        public const int PROCESSING = 2;
        public const int PAID = 3;
        public const int CANCEL = 4;
    }

    public class Orders
    {
        public int orderId;
        public Customer? customerId;
        public DateTime orderDate;
        public int orderStatus;
        public List<OrderDetails>? booksList;
        public OrderDetails this[int index]
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
                if (booksList == null) booksList = new List<OrderDetails>();
                booksList.Add(value);
            }
        }

        public Orders()
        {
            booksList = new List<OrderDetails>();
        }
    }

    public class Payment
    {
        public decimal paymentAmount;
        public decimal refund;
    }
}