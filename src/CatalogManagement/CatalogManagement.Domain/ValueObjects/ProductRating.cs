using Common.DomainCommon.ValueObjects;

namespace CatalogManagement.Domain.ValueObjects;

/// <summary>
/// Represents a product rating in the system.
/// </summary>
public class ProductRating(decimal rate, int count) : ValueObject
{
    /// <summary>
    /// Gets or sets the product's rating.
    /// Must be between 0 and 5, with no more than one decimal place.
    /// </summary>
    public decimal Rate { get; set; } = rate;

    /// <summary>
    /// Gets or sets the product's rating count.
    /// Must be a positive integer.
    /// </summary>
    public int Count { get; set; } = count;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Rate;
        yield return Count;
    }

    public static implicit operator string(ProductRating rate) => $"{rate.Rate:0.0}/{rate.Count}";
}
