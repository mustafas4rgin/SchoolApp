using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class SurveyConfiguration : IEntityTypeConfiguration<Survey>
{
    public void Configure(EntityTypeBuilder<Survey> builder)
    {
        builder.ToTable("Surveys");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Title).IsRequired();

        builder.HasMany(s => s.Questions)
            .WithOne(sq => sq.Survey)
            .HasForeignKey(sq => sq.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(s => s.AnsweredStudents)
            .WithOne(ss => ss.Survey)
            .HasForeignKey(ss => ss.SurveyId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}