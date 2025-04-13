using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class OptionMap : IEntityTypeConfiguration<OptionEntity>
{
    public void Configure(EntityTypeBuilder<OptionEntity> builder)
    {
        builder.ToTable("Option","dbo");

        builder.HasKey(o => o.Key);

        builder.Property(o => o.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(o=>o.Text).HasColumnName("Text").HasMaxLength(4000).IsRequired();

        builder.HasOne(o => o.Question).WithMany(q => q.Options).HasForeignKey(o => o.QuestionRef).IsRequired();
    }
}