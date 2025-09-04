using FluentValidation;

namespace CatalogManagement.Application.Branches.CreateBranch;

/// <summary>
/// Validator for CreateBranchCommand that defines validation rules for Branch creation command.
/// </summary>
public class CreateBranchValidator : AbstractValidator<CreateBranchCommand>
{
    /// <summary>
    /// Initializes a new instance of the CreateBranchValidator with defined validation rules.
    /// </summary>
    public CreateBranchValidator()
    {
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
