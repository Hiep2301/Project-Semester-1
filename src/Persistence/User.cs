namespace Persistence
{
    public class User
    {
        private string? userName;
        private string? password;

        public string? getUserName()
        {
            return this.userName;
        }

        public void setUserName(string? userName)
        {
            this.userName = userName;
        }

        public string? getPassword()
        {
            return this.password;
        }

        public void setPassword(string? password)
        {
            this.password = password;
        }



    }
}