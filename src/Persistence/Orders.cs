namespace Persistence
{
    public class OrderDetails
    {
        public Book book = new Book();
        public int quantity;
    }

    public class Orders
    {
        public int orderId;
        public Customer? customerId;
        public DateTime orderDate;
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