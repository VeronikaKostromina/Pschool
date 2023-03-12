using Microsoft.EntityFrameworkCore;
using Pschool.Shared.Models;

namespace Pschool.Configurations
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Document> builder)
        {
            builder.ToTable(Constants.DocumentTableName);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.FileName).IsRequired();
            builder
                .HasOne(x => x.Student)
                .WithOne(x => x.Document);
        }
    }
}
