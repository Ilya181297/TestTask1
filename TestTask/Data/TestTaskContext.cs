#nullable disable
using Microsoft.EntityFrameworkCore;
using TestTask.Models;

namespace TestTask.Data
{
    /// <summary>
    /// Контекст для работы с базой данных
    /// </summary>
    public class TestTaskContext : DbContext
    {
        /// <summary>
        /// Конструктор для инициализации контекста
        /// </summary>
        /// <param name="options">Настройки контекста</param>
        public TestTaskContext(DbContextOptions<TestTaskContext> options)
           : base(options) { }

        /// <summary>
        /// Набор данных подразделений
        /// </summary>
        public DbSet<Division> Division { get; set; }

        /// <summary>
        /// Набор данных работников
        /// </summary>
        public DbSet<Worker> Worker { get; set; }

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
