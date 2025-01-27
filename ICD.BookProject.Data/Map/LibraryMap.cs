using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class LibraryMap : IEntityTypeConfiguration<LibraryEntity>
{
    public void Configure(EntityTypeBuilder<LibraryEntity> builder)
    {
        builder.ToTable("Library","dbo");

        builder.HasKey(l => l.Key);

        builder.Property(l => l.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();

        builder.HasOne(l => l.User).WithMany(u => u.BookLibraries).HasForeignKey(l => l.UserRef);
        builder.HasOne(l=>l.Book).WithMany(b=>b.UserLibraries).HasForeignKey(l=>l.BookRef);

    }
}