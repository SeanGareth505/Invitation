using Microsoft.EntityFrameworkCore;

namespace InivitationApplication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<InvitationModel> Invitations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        // DbSets represent tables in your database.
        // Example: public DbSet<YourEntity> YourEntities { get; set; }
    }
}
