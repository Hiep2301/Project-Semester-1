namespace Persistence
{
    public static class OrderStatus
    {
        public const int UNPAID = 1;
        public const int PROCESSING = 2;
        public const int PAID = 3;
        public const int CANCEL = 4;
    }

    public class Order
    {
        private int orderId;
        private int customerId;
        private string? orderDate;
        private int orderStatus;
        private List<Book>? listBook;

        public Order()
        {

        }

        public Order(int orderId, int customerId, string orderDate, int orderStatus)
        {
            this.orderId = orderId;
            this.customerId = customerId;
            this.orderDate = orderDate;
            this.orderStatus = orderStatus;
        }

        public int getOrderId()
        {
            return this.orderId;
        }

        public void setOrderId(int orderId)
        {
            this.orderId = orderId;
        }

        public int getCustomerId()
        {
            return this.customerId;
        }

        public void setCustomerId(int customerId)
        {
            this.customerId = customerId;
        }

        public string? getOrderDate()
        {
            return this.orderDate;
        }

        public void setOrderDate(string orderDate)
        {
            this.orderDate = orderDate;
        }

        public int getOrderStatus()
        {
            return this.orderStatus;
        }

        public void setOrderStatus(int orderStatus)
        {
            this.orderStatus = orderStatus;
        }
        
        public List<Book>? getListBook()
        {
            return this.listBook;
        }

        public void setListBook(List<Book>? listBook)
        {
            this.listBook = listBook;
        }
    }
}