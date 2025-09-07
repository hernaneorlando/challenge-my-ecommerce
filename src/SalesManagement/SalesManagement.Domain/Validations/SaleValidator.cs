using FluentValidation;
using SalesManagement.Domain.Entities;
using SalesManagement.Domain.Enums;

namespace SalesManagement.Domain.Validations;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(s => s.Number).NotEmpty();
        RuleFor(c => c.Branch).NotNull();

        RuleFor(c => c.Items).NotEmpty();

        When(c => c.Date is not null, () =>
        {
            RuleFor(c => c.Status)
                .Must(status => status == SaleStatus.Completed)
                .WithMessage("Sale must be completed to have a closing date");
        });
    }
}
