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
        public TestTaskContext (DbContextOptions<TestTaskContext> options)
            : base(options)
        {
        }

        public DbSet<TestTask.Models.Division?> Division { get; set; }
    }
}
