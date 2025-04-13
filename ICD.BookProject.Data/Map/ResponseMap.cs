using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class ResponseMap : IEntityTypeConfiguration<ResponseEntity>
{
    public void Configure(EntityTypeBuilder<ResponseEntity> builder)
    {
        builder.ToTable("Response","dbo");

        builder.HasKey(r => r.Key);

        builder.Property(r => r.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(r=>r.Text).HasMaxLength(4000).HasColumnName("Text").IsRequired(false);
        builder.Property(r=>r.Digit).HasColumnName("Digit").IsRequired(false);

        builder.HasOne(r => r.User).WithMany(u => u.Responses).HasForeignKey(r => r.UserRef).IsRequired();
        builder.HasOne(r=>r.Option).WithMany(o => o.Responses).HasForeignKey(r => r.OptionRef).IsRequired(false);
        builder.HasOne(r=>r.Question).WithMany(q=>q.Responses).HasForeignKey(r => r.QuestionRef).IsRequired();
    }
}