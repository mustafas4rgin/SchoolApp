using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class ScholarshipApplicationConfiguration : IEntityTypeConfiguration<ScholarshipApplication>
{
    public void Configure(EntityTypeBuilder<ScholarshipApplication> builder)
    {
        builder.ToTable("ScholarshipApplications");

        builder.HasKey(sa => sa.Id);

        builder.HasOne(sa => sa.Student)
            .WithMany()
            .HasForeignKey(sa => sa.StudentId)
            .OnDelete(DeleteBehavior.NoAction);

    }
}