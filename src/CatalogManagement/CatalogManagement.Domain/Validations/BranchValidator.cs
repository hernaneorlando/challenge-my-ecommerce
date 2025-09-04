using CatalogManagement.Domain.Entities;
using FluentValidation;

namespace CatalogManagement.Domain.Validations;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(branch => branch.Name)
            .NotEmpty();

        RuleFor(branch => branch.Code)
            .NotEmpty();

        RuleFor(branch => branch.Description)
            .NotEmpty();
            
        When(branch => !string.IsNullOrEmpty(branch.Address), () =>
        {
            RuleFor(branch => branch.Address)
                .MinimumLength(10).WithMessage("Address must be at least 10 characters long.")
                .MaximumLength(100).WithMessage("Address must not exceed 100 characters.");
        });
    }
}
