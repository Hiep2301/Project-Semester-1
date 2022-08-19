namespace Persistence
{
    public class Customer : User
    {
        public int customerId;
        public string? customerName;
        public string? phone;
        public string? address;

        public Customer()
        {

        }

        public Customer(string userName, string password, int customerId, string? customerName, string? phone, string? address)
        {
            this.userName = userName;
            this.password = password;
            this.customerId = customerId;
            this.customerName = customerName;
            this.phone = phone;
            this.address = address;
        }

        public override string ToString()
        {
            return $"{this.userName} - {this.password} - {this.customerId} - {this.customerName} {this.phone} - {this.address}";
        }
    }
}