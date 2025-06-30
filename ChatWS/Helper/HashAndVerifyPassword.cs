using BCrypt.Net;
namespace ChatWS.Helper
{
    public class HashAndVerifyPassword
    {
        public string HashPassword(string PlainPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(PlainPassword);
        }

        public bool VerifyPassword(string PlainPassword, string HashedPassword) { 
            return BCrypt.Net.BCrypt.Verify(PlainPassword, HashedPassword);
        
        }
    
    }

}
