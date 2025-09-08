using Bogus;
using CatalogManagement.Domain.Entities;
using CatalogManagement.Domain.ValueObjects;

namespace CatalogManagement.Unit.Domain.Entities.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public static class ProductTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Product entities.
    /// The generated suppliers will have valid:
    /// - Name (using company names)
    /// - Registration Number (meeting complexity requirements)
    /// - Email (valid format)
    /// - Phone (Brazilian format)
    /// - Status (Active or Suspended)
    /// - Role (Product or Admin)
    /// </summary>
    private static Faker<Product> ProductFaker
    {
        get
        {
            var supplierId = Guid.NewGuid();
            return new Faker<Product>()
                .RuleFor(p => p.Title, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Category, f => f.Commerce.Categories(1).First())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(1, 100, 2)))
                .RuleFor(p => p.SupplierId, _ => supplierId)
                .RuleFor(p => p.Supplier, _ =>
                {
                    var supplier = SupplierTestData.GenerateValidSupplier();
                    supplier.Id = supplierId;
                    return supplier;
                })
                .RuleFor(p => p.Rating, f => new ProductRating(f.Random.Decimal(1, 5), f.Random.Int()));
        }
    }

    /// <summary>
    /// Generates a valid Product entity with randomized data.
    /// The generated Product will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Product entity with randomly generated data.</returns>
    public static Product GenerateValidProduct()
    {
        return ProductFaker.Generate();
    }

    public static string GenerateMinimalInvalidDescription(int textSize)
    {
        return new Faker().Lorem.Sentence(textSize)[..textSize];
    }
}
