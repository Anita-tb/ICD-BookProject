using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class QuestionMap : IEntityTypeConfiguration<QuestionEntity>
{
    public void Configure(EntityTypeBuilder<QuestionEntity> builder)
    {
        builder.ToTable("Question","dbo");

        builder.HasKey(q => q.Key);

        builder.Property(q=>q.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(q=>q.Text).HasColumnName("Text").HasMaxLength(4000).IsRequired();
        builder.Property(q=>q.Type).HasColumnName("Type").HasMaxLength(200).IsRequired();

        builder.HasOne(q => q.Questionnaire).WithMany(q => q.Questions).HasForeignKey(q => q.QuestionnaireRef)
            .IsRequired();

    }
}