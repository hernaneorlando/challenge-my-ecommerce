using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Domain.Entities;

namespace SalesManagement.ORM.Mapping;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SalesItems");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.OwnsOne(s => s.Product, productBuilder =>
        {
            productBuilder.Property(b => b.Id)
                .HasColumnName("ProductId")
                .HasColumnType("uuid")
                .IsRequired();

            productBuilder.Property(b => b.Name)
                .HasColumnName("ProductName")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsOne(s => s.Supplier, supplierBuilder =>
        {
            supplierBuilder.Property(b => b.Id)
                .HasColumnName("SupplierId")
                .HasColumnType("uuid")
                .IsRequired();

            supplierBuilder.Property(b => b.Name)
                .HasColumnName("SupplierName")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.Property(s => s.Quantity).IsRequired();

        builder.Property(s => s.UnitPrice)
            .IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(s => s.Discount)
            .IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(s => s.TotalAmount)
            .IsRequired().HasColumnType("decimal(18,2)");

        builder.HasOne(s => s.Sale)
            .WithMany(s => s.Items)
            .HasForeignKey(s => s.SaleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
