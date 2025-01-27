using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Driver.Core.Connections;

namespace ICD.BookProject.Data.Map;

public class AuthorBookMap : IEntityTypeConfiguration<AuthorBookEntity>
{
    public void Configure(EntityTypeBuilder<AuthorBookEntity> builder)
    {
        builder.ToTable("AuthorBook","dbo");

        builder.HasKey(ab => ab.Key);
        
        builder.Property(ab=>ab.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        
        builder.HasOne(ab => ab.Book).WithMany(b => b.AuthorsBook).HasForeignKey(ab => ab.BookRef);
        builder.HasOne(ab=>ab.Author).WithMany(a=>a.AuthorBooks).HasForeignKey(ab=>ab.AuthorRef);
    }
}