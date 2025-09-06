using System.Linq.Expressions;
using Common.ORMCommon;
using SalesManagement.Domain.Entities;

namespace SalesManagement.Domain.Specifications;

public class OpenCartByCustomerSpecification : ISpecification<Cart>
{
    public Expression<Func<Cart, bool>> Predicate { get; private set; }

    public OpenCartByCustomerSpecification(Guid customerId)
    {
        Predicate = cart =>
            cart.Customer.Id == customerId && cart.Status == Enums.CartStatus.Open;
    }
}
