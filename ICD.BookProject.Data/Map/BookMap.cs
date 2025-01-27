using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ICD.BookProject.Data.Map
{
    public class BookMap : IEntityTypeConfiguration<BookEntity>
    {
        public void Configure(EntityTypeBuilder<BookEntity> builder)
        {
            builder.ToTable("Book", "dbo");

            builder.HasKey(b => b.Key);

            builder.Property(b => b.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
            builder.Property(b => b.Title).HasColumnName("Title").HasMaxLength(50).IsRequired();
            builder.Property(b=>b.Page).HasColumnName("Page").IsRequired();
      
            builder.HasOne(b=>b.Category).WithMany(c=>c.Books).HasForeignKey(b=>b.CategoryRef).IsRequired(false);
        }
    }
}
