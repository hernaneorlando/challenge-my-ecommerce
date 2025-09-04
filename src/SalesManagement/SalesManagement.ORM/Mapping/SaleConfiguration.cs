using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Domain.Entities;

namespace SalesManagement.ORM.Mapping;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(s => s.Number)
            .IsRequired().HasMaxLength(100);

        builder.Property(s => s.Date);

        builder.OwnsOne(s => s.Branch, branchBuilder =>
        {
            branchBuilder.Property(b => b.Id)
                .HasColumnName("BranchId")
                .HasColumnType("uuid")
                .IsRequired();

            branchBuilder.Property(b => b.Name)
                .HasColumnName("BranchName")
                .IsRequired()
                .HasMaxLength(100);

            branchBuilder.Property(b => b.Code)
                .HasColumnName("BranchCode")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.Property(s => s.TotalAmount)
            .IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(s => s.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasMany(s => s.Items)
            .WithOne(s => s.Sale)
            .HasForeignKey(s => s.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
