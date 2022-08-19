namespace Persistence
{
    public class Admin : User
    {
        private int adminId;
        private string? admintName;
        private string? phone;

        public Admin()
        {

        }

        public Admin(string userName, string password)
        {
            this.setUserName(userName);
            this.setPassword(password);
        }

        public Admin(string userName, string password, int adminId, string admintName, string phone)
        {
            this.setUserName(userName);
            this.setPassword(password);
            this.adminId = adminId;
            this.admintName = admintName;
            this.phone = phone;
        }

        public int getAdminId()
        {
            return this.adminId;
        }

        public void setAdminId(int adminId)
        {
            this.adminId = adminId;
        }

        public string? getAdmintName()
        {
            return this.admintName;
        }

        public void setAdmintName(string? admintName)
        {
            this.admintName = admintName;
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
            return $"{this.getUserName()} - {this.getPassword()} - {this.getAdminId()} - {this.getAdmintName()} - {this.getPhone()}";
        }
    }
}