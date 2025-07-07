
using ChatWS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Identity.Client;
namespace ChatWS.Data
{
   public class AppDbContext : DbContext
    {
        // Creates a connection to the DB
    public AppDbContext(DbContextOptions<AppDbContext>options) : base(options)
        {
           
        }
        public DbSet<User> users { get; set; }

    }

    

   
}
    

