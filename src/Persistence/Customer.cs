namespace Persistence
{
    public class Customer : User
    {
        private int customerId;
        private string? customerName;
        private string? phone;
        private string? address;

        public Customer()
        {

        }

        public Customer(string userName, string password, int customerId, string? customerName, string? phone, string? address)
        {
            this.setUserName(userName);
            this.setPassword(password);
            this.customerId = customerId;
            this.customerName = customerName;
            this.phone = phone;
            this.address = address;
        }

        public int getCustomerId()
        {
            return this.customerId;
        }

        public void setCustomerId(int customerId)
        {
            this.customerId = customerId;
        }

        public string? getCustomerName()
        {
            return this.customerName;
        }

        public void setCustomerName(string? customerName)
        {
            this.customerName = customerName;
        }

        public string? getPhone()
        {
            return this.phone;
        }

        public void setPhone(string? phone)
        {
            this.phone = phone;
        }

        public string? getAddress()
        {
            return this.address;
        }

        public void setAddress(string? address)
        {
            this.address = address;
        }

        public override string ToString()
        {
            return $"{this.getUserName()} - {this.getPassword()} - {this.getCustomerId()} - {this.getCustomerName()} {this.getPhone()} - {this.getAddress()}";
        }
    }
}