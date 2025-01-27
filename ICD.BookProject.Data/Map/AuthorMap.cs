using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class AuthorMap : IEntityTypeConfiguration<AuthorEntity>
{
    public void Configure(EntityTypeBuilder<AuthorEntity> builder)
    {
        builder.ToTable("Author","dbo");

        builder.HasKey(a => a.Key);
        
        builder.Property(a=>a.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(a=>a.Name).HasColumnName("Name").HasMaxLength(100).IsRequired();
    }
}