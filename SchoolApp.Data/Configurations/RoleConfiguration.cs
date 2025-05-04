using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");

        builder.HasKey(r => r.Id);
        builder.Property(r => r.Name).IsRequired();
        builder.Property(r => r.CreatedAt).IsRequired();

        builder.HasMany(r => r.Users)
           .WithOne(u => u.Role)
           .HasForeignKey(u => u.RoleId)
           .OnDelete(DeleteBehavior.Restrict);       
    }
}