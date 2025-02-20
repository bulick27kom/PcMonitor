using Microsoft.EntityFrameworkCore;
using PcMonitorWebApi.Data.Models;

namespace PcMonitorWebApi.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Computer> Computers { get; set; }
        public DbSet<Group> Groups { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей
            modelBuilder.Entity<Computer>()
                .HasOne(c => c.Group)
                .WithMany(g => g.Computers)
                .HasForeignKey(c => c.GroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
