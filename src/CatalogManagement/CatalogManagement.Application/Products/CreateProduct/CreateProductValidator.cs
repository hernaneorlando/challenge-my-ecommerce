using FluentValidation;

namespace CatalogManagement.Application.Products.CreateProduct;

/// <summary>
/// Validator for CreateProductCommand that defines validation rules for Product creation command.
/// </summary>
public class CreateProductValidator : AbstractValidator<CreateProductCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateProductValidator with defined validation rules.
    /// </summary>
    public CreateProductValidator()
    {
        RuleFor(product => product.Title).NotEmpty();
        RuleFor(product => product.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(product => product.SupplierId)
            .NotEqual(Guid.Empty).WithMessage("SupplierId must be a valid GUID.");

        When(product => !string.IsNullOrWhiteSpace(product.Description), () =>
        {
            RuleFor(product => product.Description)
                .MinimumLength(10).WithMessage("Description must be at least 10 characters long.")
                .MaximumLength(256).WithMessage("Description cannot be longer than 256 characters.");
        });

        When(product => !string.IsNullOrWhiteSpace(product.Category), () =>
        {
            RuleFor(product => product.Category)
                .MinimumLength(3).WithMessage("Category must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Category cannot be longer than 100 characters.");
        });

        When(product => !string.IsNullOrWhiteSpace(product.Image), () =>
        {
            RuleFor(product => product.Image)
                .MaximumLength(500).WithMessage("Image URL cannot be longer than 500 characters.")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Image must be a valid URL.");
        });
    }
}
