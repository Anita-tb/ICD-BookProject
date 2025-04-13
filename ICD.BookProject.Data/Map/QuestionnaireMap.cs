using ICD.BookProject.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.BookProject.Data.Map;

public class QuestionnaireMap : IEntityTypeConfiguration<QuestionnaireEntity>
{
    public void Configure(EntityTypeBuilder<QuestionnaireEntity> builder)
    {
        builder.ToTable("Questionnaire" , "dbo");
        
        builder.HasKey(q => q.Key);
        
        builder.Property(q=>q.Key).HasColumnName("Id").ValueGeneratedOnAdd().IsRequired();
        builder.Property(b => b.Title).HasColumnName("Title").HasMaxLength(200).IsRequired();
        builder.Property(b => b.Description).HasColumnName("Description").HasMaxLength(4000).IsRequired(false);
        
    }
}