using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolApp.Domain.Entities;

namespace SchoolApp.Data.Configurations;

public class TuitionPaymentConfiguration : IEntityTypeConfiguration<TuitionPayment>
{
    public void Configure(EntityTypeBuilder<TuitionPayment> builder)
    {
        builder.ToTable("TuitionPayments");

        builder.HasKey(tp => tp.Id);

        builder.Property(tp => tp.StudentId).IsRequired();

        builder.HasOne(tp => tp.Student)
            .WithMany()
            .HasForeignKey(tp => tp.StudentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}