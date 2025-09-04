using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesManagement.Domain.Entities;

namespace SalesManagement.ORM.Mapping;

public class CartConfiguration : IEntityTypeConfiguration<Cart>
{
    public void Configure(EntityTypeBuilder<Cart> builder)
    {
        builder.ToTable("Carts");

        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");

        builder.OwnsOne(c => c.Customer, customerBuilder =>
        {
            customerBuilder.Property(c => c.Id)
                .HasColumnName("CustomerId")
                .HasColumnType("uuid")
                .IsRequired();

            customerBuilder.Property(c => c.Name)
                .HasColumnName("CustomerName")
                .IsRequired()
                .HasMaxLength(100);
        });

        builder.OwnsOne(c => c.Branch, branchBuilder =>
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

        builder.Property(c => c.CheckoutDate)
            .HasColumnType("DATE");

        builder.Property(c => c.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasMany(c => c.Items)
            .WithOne(ci => ci.Cart)
            .HasForeignKey(ci => ci.CartId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
