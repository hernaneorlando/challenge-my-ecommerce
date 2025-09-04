using CatalogManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogManagement.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(p => p.Title)
            .IsRequired().HasMaxLength(100);

        builder.Property(p => p.Price)
            .IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(p => p.Description).HasMaxLength(256);
        builder.Property(p => p.Category).HasMaxLength(100);
        builder.Property(p => p.Image).HasMaxLength(500);

        builder.OwnsOne(p => p.Rating, ratingBuilder =>
        {
            ratingBuilder.Property(r => r.Rate)
                .HasColumnName("Rating")
                .HasColumnType("decimal(2,1)")
                .HasDefaultValue(0);

            ratingBuilder.Property(r => r.Count)
                .HasColumnName("RatingCount")
                .HasDefaultValue(0);
        });

        builder.HasOne(p => p.Supplier)
            .WithMany(s => s.Products)
            .HasForeignKey(p => p.SupplierId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasIndex(p => new { p.Title, p.Price, p.SupplierId })
            .IsUnique();
    }
}
