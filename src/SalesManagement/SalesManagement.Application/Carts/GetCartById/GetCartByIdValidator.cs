using FluentValidation;

namespace SalesManagement.Application.Carts.GetCartById;

/// <summary>
/// Validator for GetCartByIdQuery
/// </summary>
public class GetCartByIdValidator : AbstractValidator<GetCartByIdQuery>
{
    public GetCartByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required");
    }
}
