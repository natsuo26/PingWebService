namespace ChatWS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; }
        public string Password { get; set; }

        public User(string userName, string password)
        {
            this.userName = userName;
            Password = password;
        }
    }
}
