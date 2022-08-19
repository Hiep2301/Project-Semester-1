namespace Persistence
{
    public class Admin : User
    {
        public int adminId;
        public string? admintName;
        public string? phone;

        public Admin()
        {

        }

        public Admin(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
        }

        public Admin(string userName, string password, int adminId, string admintName, string phone)
        {
            this.userName = userName;
            this.password = password;
            this.adminId = adminId;
            this.admintName = admintName;
            this.phone = phone;
        }

        public override string ToString()
        {
            return $"{this.userName} - {this.password} - {this.adminId} - {this.admintName} - {this.phone}";
        }
    }
}