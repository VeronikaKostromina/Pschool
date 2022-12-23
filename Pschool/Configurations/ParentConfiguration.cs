using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pschool.Shared.Models;

namespace Pschool.Configurations
{
    public class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.ToTable(Constants.ParentsTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName).IsRequired();
            builder.Property(x => x.LastName).IsRequired();
            builder.Property(x => x.Email).IsRequired();

            builder
                .HasMany(x => x.Students)
                .WithOne(s => s.Parent)
                .HasForeignKey(s => s.ParentId);
        }
    }
}
