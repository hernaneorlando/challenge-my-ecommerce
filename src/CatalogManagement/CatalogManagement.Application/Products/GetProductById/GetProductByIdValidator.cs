using FluentValidation;

namespace CatalogManagement.Application.Products.GetProductById;

/// <summary>
/// Validator for GetProductByIdCommand
/// </summary>
public class GetProductByIdValidator: AbstractValidator<GetProductByIdQuery>
{
    /// <summary>
    /// Initializes validation rules for GetProductByIdCommand
    /// </summary>
    public GetProductByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}
