using Microsoft.EntityFrameworkCore;
using Pschool.Configurations;
using Pschool.Shared.Models;

namespace Pschool
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Parent> Parents { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<Student>(new StudentConfiguration());
            modelBuilder.ApplyConfiguration<Parent>(new ParentConfiguration());
        }
    }
}
