using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class SurveyOptionConfiguration : IEntityTypeConfiguration<SurveyOption>
{
    public void Configure(EntityTypeBuilder<SurveyOption> builder)
    {
        builder.ToTable("SurveyOptions");

        builder.HasKey(so => so.Id);

        builder.Property(so => so.QuestionId).IsRequired();
        builder.Property(so => so.Text).IsRequired();

        builder.HasOne(so => so.Question)
            .WithMany(sq => sq.Options)
            .HasForeignKey(so => so.QuestionId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}