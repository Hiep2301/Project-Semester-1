namespace Persistence
{
    public class Customer : User
    {
        public int customerId;
        public string? customerName;
        public string? phone;
        public string? address;

        public override string ToString()
        {
            return $"{this.userName} - {this.password} - {this.customerId} - {this.customerName} {this.phone} - {this.address}";
        }
    }
}