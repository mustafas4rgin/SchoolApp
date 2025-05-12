using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class SurveyAnswerConfiguration : IEntityTypeConfiguration<SurveyAnswer>
{
    public void Configure(EntityTypeBuilder<SurveyAnswer> builder)
    {
        builder.ToTable("SurveyAnswers");

        builder.HasKey(sa => sa.Id);

        builder.Property(sa => sa.QuestionId).IsRequired();
        builder.Property(sa => sa.SelectedOptionId).IsRequired();
        builder.Property(sa => sa.StudentId).IsRequired();

        builder.HasOne(sa => sa.Option)
            .WithMany()
            .HasForeignKey(sa => sa.SelectedOptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(sa => sa.Question)
            .WithMany()
            .HasForeignKey(sa => sa.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}