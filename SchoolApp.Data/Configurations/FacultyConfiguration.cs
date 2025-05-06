using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class FacultyConfiguration : IEntityTypeConfiguration<Faculty>
{
    public void Configure(EntityTypeBuilder<Faculty> builder)
    {
        builder.ToTable("Faculties");

        builder.Property(f => f.Id).IsRequired();
        builder.Property(f => f.CreatedAt).IsRequired();
        builder.Property(f => f.Title).IsRequired();
        
        builder.HasMany(f => f.Departments)
            .WithOne(d => d.Faculty)
            .HasForeignKey(d => d.FacultyId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}