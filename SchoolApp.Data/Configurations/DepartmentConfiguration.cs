using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
{
    public void Configure(EntityTypeBuilder<Department> builder)
    {
        builder.ToTable("Departments");

        builder.Property(d => d.Id).IsRequired();
        builder.Property(d => d.CreatedAt).IsRequired();
        builder.Property(d => d.Title).IsRequired();

        builder.HasOne(d => d.Faculty)
            .WithMany(f => f.Departments)
            .HasForeignKey(d => d.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(d => d.Teachers)
            .WithOne(t => t.Department)
            .HasForeignKey(t => t.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(d => d.Students)
            .WithOne(s => s.Department)
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany(d => d.Courses)
            .WithOne(c => c.Department)
            .HasForeignKey(c => c.DepartmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}