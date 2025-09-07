using Bogus;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Integration.Sales.TestData;

/// <summary>
/// Provides methods for generating test data using the Bogus library.
/// This class centralizes all test data generation to ensure consistency
/// across test cases and provide both valid and invalid data scenarios.
/// </summary>
public class SaleFeatureTestData
{
    /// <summary>
    /// Configures the Faker to generate valid Sale entities.
    /// The generated users will have valid:
    /// - Sale's number
    /// - Date when the sale was made
    /// - Customer
    /// - Branch where the sale was made
    /// - Products
    /// - Quantities
    /// - Unit prices
    /// - Discounts
    /// - Total amount for each item
    /// - Cancelled/Not Cancelled
    /// </summary>
    private static readonly Faker<Sale> createSaleFaker = new Faker<Sale>()
        .RuleFor(u => u.Number, f => Sale.GenerateSaleNumber())
        .RuleFor(u => u.Branch, _ => createSaleBranchFaker!.Generate())
        .RuleFor(u => u.TotalAmount, f => f.Random.Decimal(1, 10000))
        .RuleFor(u => u.Status, f => f.PickRandom(SaleStatus.Pending))
        .RuleFor(u => u.Items, f => createSaleItemFaker!.Generate(5));

    private static readonly Faker<SaleBranch> createSaleBranchFaker = new Faker<SaleBranch>()
        .RuleFor(u => u.Name, f => $"{f.Company.CompanyName()} {f.Address.City()}")
        .RuleFor(u => u.Code, f => f.Company.CompanySuffix());

    private static readonly Faker<SaleItem> createSaleItemFaker = new Faker<SaleItem>()
        .RuleFor(u => u.Product, _ => createSaleProductFaker!.Generate())
        .RuleFor(u => u.Supplier, f => createSaleSupplierFaker!.Generate())
        .RuleFor(u => u.Quantity, f => f.Random.Int())
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(1, 100))
        .RuleFor(u => u.TotalAmount, f => f.Random.Decimal(1, 1000));

    private static readonly Faker<SaleProduct> createSaleProductFaker = new Faker<SaleProduct>()
        .RuleFor(u => u.Name, f => f.Commerce.ProductName());

    private static readonly Faker<SaleSupplier> createSaleSupplierFaker = new Faker<SaleSupplier>()
        .RuleFor(u => u.Name, f => f.Company.CompanyName());

    /// <summary>
    /// Generates a valid Sale entity with randomized data.
    /// The generated user will have all properties populated with valid values
    /// that meet the system's validation requirements.
    /// </summary>
    /// <returns>A valid Sale entity with randomly generated data.</returns>
    public static Sale GenerateValidSale()
    {
        return createSaleFaker.Generate();
    }
}
