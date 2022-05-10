#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public DbSet<TestTask.Models.Division> Division { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Division>(entity =>
            {
                entity
                    .HasMany(e => e.Children)
                    .WithOne(e => e.Parent) 
                    .HasForeignKey(e => e.ParentId)
                    .OnDelete(DeleteBehavior.NoAction);
            });
        }
    }
}
