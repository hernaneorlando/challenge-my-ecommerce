using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Domain.Entities;

namespace SalesManagement.ORM.Mapping;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.Property(c => c.ProductId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(c => c.SupplierId)
            .HasColumnType("uuid")
            .IsRequired();

        builder.Property(c => c.Quantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.Property(c => c.UnitPrice)
            .IsRequired().HasColumnType("decimal(18,2)");

        builder.Property(c => c.Discount)
            .IsRequired().HasColumnType("decimal(18,2)");

        builder.HasOne(c => c.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(c => c.CartId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
