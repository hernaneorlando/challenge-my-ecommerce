using CatalogManagement.Domain.Entities;
using FluentValidation;

namespace CatalogManagement.Domain.Validations;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Title)
            .NotEmpty();

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
