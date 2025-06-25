using Microsoft.EntityFrameworkCore;
// pull request test
namespace Chat_App_API.Models
{
    public class AppDbContext : DbContext
    {
        internal static object applicationDbContext;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { 
        }
        public DbSet<UsersTable> Users { get; set; }


    } 
   
}
    
