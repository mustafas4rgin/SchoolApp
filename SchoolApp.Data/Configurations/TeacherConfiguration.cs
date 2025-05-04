using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.ToTable("Teachers");
        builder.HasBaseType<User>();

        builder.Property(t => t.CreatedAt).IsRequired();
        builder.Property(t => t.Email)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(t => t.FirstName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(t => t.Hash).IsRequired();
        builder.Property(t => t.LastName)
            .IsRequired()
            .HasMaxLength(50);
        builder.Property(t => t.Phone)
            .IsRequired()
            .HasMaxLength(15);
        builder.Property(t => t.Salt).IsRequired();
        builder.Property(t => t.Number)
            .IsRequired();

        builder.HasMany(t => t.Courses)
            .WithOne(c => c.Teacher)
            .HasForeignKey(c => c.TeacherId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}