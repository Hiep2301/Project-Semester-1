namespace Persistence
{
    public class Admin : User
    {
        private string? firstName;
        private string? lastName;
        private string? phone;

        public Admin()
        {

        }

        public Admin(string userName, string password, string firstName, string lastName, string phone)
        {
            this.setUserName(userName);
            this.setPassword(password);
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
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

        public override string ToString()
        {
            return $"{this.firstName} {this.lastName} {this.phone}";
        }
    }
}