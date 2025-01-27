using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class CategoryMap : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        builder.ToTable("Category", "dbo");

        builder.HasKey(c => c.Key);

        builder.Property(c => c.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(c => c.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
        
    }
}