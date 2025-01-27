using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class UserMap : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("User","dbo");

        builder.HasKey(u => u.Key);
        
        builder.Property(u=>u.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(u=>u.Username).HasColumnName("Username").HasMaxLength(50).IsRequired();
        builder.Property(u=>u.Password).HasColumnName("Password").HasMaxLength(100).IsRequired();
        
    }
}