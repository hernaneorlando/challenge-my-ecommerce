using System;

namespace SalesManagement.Domain.Common;

public static class DiscountCalculationHelper
{
    public static void CalculateDiscount<TEntity>(ICollection<TEntity> items)
        where TEntity : IItemWithDiscount
    {
        foreach (var item in items)
        {
            item.Discount = item.Quantity switch
            {
                _ when item.Quantity >= 4 && item.Quantity < 10 => 0.1M,
                _ when item.Quantity >= 10 && item.Quantity < 20 => 0.2M,
                _ => 0.0M
            };
        }
    }
}
