using FluentValidation;

namespace SalesManagement.Application.Carts.GetCartById;

/// <summary>
/// Validator for GetCartByIdCommand
/// </summary>
public class GetCartByIdValidator : AbstractValidator<GetCartByIdQuery>
{
    /// <summary>
    /// Initializes validation rules for GetCartByIdCommand
    /// </summary>
    public GetCartByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Cart ID is required");
    }
}
