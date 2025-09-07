using FluentValidation;

namespace SalesManagement.Application.Sales.GetSaleById;

/// <summary>
/// Validator for GetSaleByIdQuery
/// </summary>
public class GetSaleByIdValidator : AbstractValidator<GetSaleByIdQuery>
{
    public GetSaleByIdValidator()
    {
        RuleFor(s => s.Id)
            .NotNull().NotEqual(Guid.Empty);
    }
}
