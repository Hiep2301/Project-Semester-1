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

    public class Order
    {
        public int orderId;
        public Customer? customerId;
        public DateTime orderDate;
        public int orderStatus;
        public List<Book>? listBook;

        public Order()
        {

        }

        public Order(int orderId, Customer customerId, DateTime orderDate, int orderStatus)
        {
            this.orderId = orderId;
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.orderStatus = orderStatus;
        }
    }
}