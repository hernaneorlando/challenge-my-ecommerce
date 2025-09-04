using System;
using FluentValidation;

namespace CatalogManagement.Application.Branches.GetBranchById;

/// <summary>
/// Validator for GetBranchByIdCommand
/// </summary>
public class GetBranchByIdValidator : AbstractValidator<GetBranchByIdQuery>
{
    /// <summary>
    /// Initializes validation rules for GetBranchByIdCommand
    /// </summary>
    public GetBranchByIdValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Branch ID is required");
    }
}
