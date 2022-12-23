using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pschool.Shared.Models;

namespace Pschool.Configurations
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable(Constants.StudentsTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.ClassNumber).IsRequired();

            builder
                .HasOne(x => x.Parent)
                .WithMany(p => p.Students)
                .HasForeignKey(x => x.ParentId);
        }
    }
}
