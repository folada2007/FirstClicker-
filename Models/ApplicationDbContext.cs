using Microsoft.EntityFrameworkCore;

namespace ClickME.Models
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options) 
        {

        }

       public DbSet<User> users { get; set; }
    }
}
