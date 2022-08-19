namespace Persistence
{
    public class Admin : User
    {
        public int adminId;
        public string? admintName;
        public string? phone;

        public override string ToString()
        {
            return $"{this.userName} - {this.password} - {this.adminId} - {this.admintName} - {this.phone}";
        }
    }
}