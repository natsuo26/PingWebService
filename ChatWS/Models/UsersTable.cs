using System.ComponentModel.DataAnnotations;

namespace Chat_App_API.Models
{
    public class UsersTable
    {
        [Key]
        public int userId { get; set; }

        public string userName { get; set; } = string.Empty;

        public string password { get; set; } = string.Empty;
        
    }
}
