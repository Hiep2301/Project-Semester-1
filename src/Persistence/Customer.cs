namespace Persistence
{
    public class Customer : User
    {
        private string? firstName;
        private string? lastName;
        private string? phone;
        private string? address;

        public Customer()
        {

        }

        public Customer(string userName, string password, string firstName, string lastName, string phone, string address)
        {
            this.setUserName(userName);
            this.setPassword(password);
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.address = address;
        }

        public string? getFirstName()
        {
            return this.firstName;
        }

        public void setFirstName(string firstName)
        {
            this.firstName = firstName;
        }

        public string? getLastName()
        {
            return this.lastName;
        }

        public void setLastName(string lastName)
        {
            this.lastName = lastName;
        }

        public string? getPhone()
        {
            return this.phone;
        }

        public void setPhone(string phone)
        {
            this.phone = phone;
        }

        public string? getAddress()
        {
            return this.address;
        }

        public void setAddress(string address)
        {
            this.address = address;
        }

        public override string ToString()
        {
            return $"{this.firstName} {this.lastName} {this.phone} {this.address}";
        }

    }
}