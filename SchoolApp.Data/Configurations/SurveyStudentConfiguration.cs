using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class SurveyStudentConfiguration : IEntityTypeConfiguration<SurveyStudent>
{
    public void Configure(EntityTypeBuilder<SurveyStudent> builder)
    {
        builder.ToTable("SurveyStudents");

        builder.HasKey(ss => ss.Id);

        builder.HasOne(ss => ss.Student)
            .WithMany(s => s.AnsweredSurveys)
            .HasForeignKey(ss => ss.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(ss => ss.Survey)
            .WithMany(s => s.AnsweredStudents)
            .HasForeignKey(ss => ss.SurveyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}