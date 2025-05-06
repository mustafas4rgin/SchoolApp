using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.ToTable("Students");
        builder.HasBaseType<User>();
        
        builder.Property(s => s.Hash).IsRequired();
        builder.Property(s => s.LastName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(s => s.Phone)
            .IsRequired()
            .HasMaxLength(15);
        builder.Property(s => s.Salt).IsRequired();
        builder.Property(s => s.Year).IsRequired();

        builder.HasMany(s => s.StudentCourses)
            .WithOne(sc => sc.Student)
            .HasForeignKey(sc => sc.StudentId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(s => s.Grades)
            .WithOne(g => g.Student)
            .HasForeignKey(g => g.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Department)
            .WithMany(d => d.Students)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}