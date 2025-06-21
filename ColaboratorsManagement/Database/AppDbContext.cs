using Microsoft.EntityFrameworkCore;

namespace ColaboratorsManagement.Database
{
    public class AppDbContext : DbContext
    {
        public DbSet<Models.CollaboratorModel> Collaborators { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
