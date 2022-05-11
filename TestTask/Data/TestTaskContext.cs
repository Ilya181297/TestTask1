#nullable disable
using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    public class TestTaskContext : DbContext
    {
        public TestTaskContext(DbContextOptions<TestTaskContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Division> Division { get; set; }
        public DbSet<Worker> Workers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Division>()
                    .HasMany(e => e.Children)
                    .WithOne(e => e.Parent)
                    .HasForeignKey(e => e.ParentId)
                    .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Worker>()
                .HasOne(p => p.Division)
                .WithMany(b => b.Workers)
                .HasForeignKey(x => x.DivisionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
