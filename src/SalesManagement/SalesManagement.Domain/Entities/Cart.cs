using Common.DomainCommon;
using Common.Validations;
using FluentValidation;
using FluentValidation.Results;
using SalesManagement.Domain.Common;
using SalesManagement.Domain.Enums;
using SalesManagement.Domain.Validations;
using SalesManagement.Domain.ValueObjects;

namespace SalesManagement.Domain.Entities;

/// <summary>
/// Represents a cart in the system with.
/// This entity follows domain-driven design principles and includes business rules validation.
/// </summary>
public class Cart : BaseEntity
{
    /// <summary>
    /// Gets or sets the customer's information.
    /// Must not be null.
    /// </summary>
    public SaleUser Customer { get; set; } = null!;

    /// <summary>
    /// Gets or sets the branch's information.
    /// Must not be null.
    /// </summary>
    public SaleBranch Branch { get; set; } = null!;

    /// <summary>
    /// Gets or sets the cart's checkout date.
    /// Can be null if the cart has not been checked out yet.
    /// </summary>
    public DateOnly? CheckoutDate { get; set; }

    /// <summary>
    /// Gets or sets the cart's status.
    /// Must not be null.
    /// </summary>
    public CartStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the cart items.
    /// </summary>
    public ICollection<CartItem> Items { get; set; } = [];

    /// <summary>
    /// Performs validation of the cart entity using the CartValidator rules.
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
        var validator = new CartValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void Create(SaleUser customer, SaleBranch branch)
    {
        Customer ??= new SaleUser { Id = customer.Id };
        Customer.Name = customer.Name;

        Branch ??= new SaleBranch { Id = branch.Id };
        Branch.Code = branch.Code;
        Branch.Name = branch.Name;

        Status = CartStatus.Open;
    }

    public void Update(Cart cart)
    {
        if (Status == CartStatus.CheckedOut)
            throw new ValidationException([new ValidationFailure(string.Empty, "The cart is already checked out.")]);

        UpdatedAt = DateTime.UtcNow;
        if (cart.CheckoutDate is null)
            return;

        CheckoutDate = cart.CheckoutDate;
        Status = CartStatus.CheckedOut;
    }

    public void AddItem(CartItem item)
    {
        var existentItem = Items
            .FirstOrDefault(i => i.ProductId.ToString().Equals(item.ProductId.ToString(), StringComparison.InvariantCultureIgnoreCase));
            
        if (existentItem is not null)
        {
            existentItem.Cart = this;
            existentItem.CartId = Id;
            existentItem.SupplierId = item.SupplierId;
            existentItem.UnitPrice = item.UnitPrice;
            existentItem.Quantity = item.Quantity;
            existentItem.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            item.Cart = this;
            item.CartId = Id;
            Items.Add(item);
        }
    }

    public void ApplyDiscount()
    {
        DiscountCalculationHelper.CalculateDiscount(Items);
    }
}
