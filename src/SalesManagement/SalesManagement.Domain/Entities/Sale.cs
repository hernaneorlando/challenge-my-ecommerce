using Common.DomainCommon;
using Common.Validations;
using FluentValidation;
using FluentValidation.Results;
using SalesManagement.Domain.Enums;
using SalesManagement.Domain.Validations;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a sale in the system.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Sale : BaseEntity
{
    /// <summary>
    /// Gets or sets the sale's number.
    /// Must not be null and not exceed 100 characters.
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date when the sale was made.
    /// Must not be null.
    /// </summary>
    public DateTime? Date { get; set; }

    /// <summary>
    /// Gets or sets the branch's information.
    /// Must not be null.
    /// </summary>
    public SaleBranch Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the total amount for each sale item.
    /// Must not be null.
    /// </summary>
    public decimal TotalAmount { get; set; }

    /// <summary>
    /// Gets or sets the sales's status.
    /// Must not be null.
    /// </summary>
    public SaleStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the sale item's information.
    /// Must not be null.
    /// </summary>
    public ICollection<SaleItem> Items { get; set; } = [];

    /// <summary>
    /// Performs validation of the sale entity using the SaleValidator rules.
    /// </summary>
    /// <returns>
    /// A <see cref="ValidationResultDetail"/> containing:
    /// - IsValid: Indicates whether all validation rules passed
    /// - Errors: Collection of validation errors if any rules failed
    /// </returns>
    /// <remarks>
    /// <listheader>The validation includes checking:</listheader>
    /// <list type="bullet">Cartname format and length</list>
    /// <list type="bullet">Email format</list>
    /// <list type="bullet">Phone number format</list>
    /// <list type="bullet">Password complexity requirements</list>
    /// <list type="bullet">Role validity</list>
    /// 
    /// </remarks>
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void Create()
    {
        if (Date is not null)
            throw new ValidationException([new ValidationFailure(string.Empty, "The sale is already completed.")]);

        Number = GenerateSaleNumber();
        TotalAmount = Items.Sum(item => item.TotalAmount);
        Status = SaleStatus.Pending;
        UpdatedAt = null;
    }

    public void AddItem(SaleProduct product, SaleSupplier supplier)
    {
        var existentItem = Items
            .FirstOrDefault(i =>
                i.Product is not null &&
                i.Product.Id.ToString().Equals(product.Id.ToString(), StringComparison.InvariantCultureIgnoreCase));

        if (existentItem is null)
            return;

        existentItem.TotalAmount = CalculateItemTotalAmount(existentItem);
        existentItem.Product = product;
        existentItem.Supplier = supplier;
        existentItem.Sale = this;
        existentItem.SaleId = Id;
    }

    private static decimal CalculateItemTotalAmount(SaleItem existentItem)
    {
        var totalValue = existentItem.UnitPrice * existentItem.Quantity;
        return totalValue - (totalValue * existentItem.Discount);
    }

    public static string GenerateSaleNumber()
    {
        var random = new Random();
        var letters = new char[3];
        for (int i = 0; i < 3; i++)
        {
            letters[i] = (char)random.Next('A', 'Z' + 1);
        }

        var guid = Guid.NewGuid();
        var base64Guid = Convert.ToBase64String(guid.ToByteArray());
        var alphanumericPart = new string([..base64Guid.Where(char.IsLetterOrDigit)])[..5].ToUpper();

        return $"{new string(letters)}{alphanumericPart}";
    }
}
