using Microsoft.EntityFrameworkCore;


namespace StaplesBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        #region Tables

        public DbSet<Client> Clients { get; set; }
        public DbSet<CurrentOrder> CurrentOrders { get; set; }
        public DbSet<ArchivedOrder> ArchivedOrders { get; set; }

        #endregion

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API
            modelBuilder.Entity<Client>().HasIndex(c => c.Login).IsUnique();
        }
    }
}
