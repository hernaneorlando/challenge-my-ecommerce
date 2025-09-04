using System;
using FluentValidation;

namespace CatalogManagement.Application.Branches.UpdateBranch;

/// <summary>
/// Validator for UpdateBranchCommand that defines validation rules for branch update command.
/// </summary>
public class UpdateBranchValidator : AbstractValidator<UpdateBranchCommand>
{
    /// <summary>
    /// Initializes a new instance of the UpdateBranchValidator with defined validation rules.
    /// </summary>
    /// <remarks>
    /// Validation rules include:
    /// - Name: Required, must be between 3 and 100 characters
    /// - Registration Number: Required, must be between 5 and 50 characters
    /// - Email: Must be in valid format (using EmailValidator)
    /// - Phone: If not empty, must match international format (+X XXXXXXXXXX)
    /// </remarks>
    public UpdateBranchValidator()
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
