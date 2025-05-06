using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder.ToTable("Courses");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.CreatedAt).IsRequired();
        builder.Property(c => c.Name).IsRequired();

        builder.HasMany(c => c.StudentCourses)
            .WithOne(c => c.Course)
            .HasForeignKey(sc => sc.CourseId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(c => c.Teacher)
            .WithMany(t => t.Courses)
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.Grades)
            .WithOne(g => g.Course)
            .HasForeignKey(g => g.CourseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.Department)
            .WithMany(d => d.Courses)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}