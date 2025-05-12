using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class SurveyQuestionConfiguration : IEntityTypeConfiguration<SurveyQuestion>
{
    public void Configure(EntityTypeBuilder<SurveyQuestion> builder)
    {
        builder.ToTable("SurveyQuestions");

        builder.HasKey(sq => sq.Id);

        builder.Property(sq => sq.QuestionText).IsRequired();

        builder.HasMany(sq => sq.Options)
            .WithOne(so => so.Question)
            .HasForeignKey(so => so.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(sq => sq.Survey)
            .WithMany()
            .HasForeignKey(sq => sq.SurveyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}