using System;
using FluentValidation;

namespace CatalogManagement.Application.Branches.DeleteBranch;

/// <summary>
/// Validator for deleting a branch
/// </summary>
public class DeleteBranchValidator : AbstractValidator<DeleteBranchCommand>
{
    /// <summary>
    /// Initializes validation rules for DeleteBranchCommand
    /// </summary>
    public DeleteBranchValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
}
