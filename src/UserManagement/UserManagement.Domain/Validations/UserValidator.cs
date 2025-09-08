using UserManagement.Domain.Entities;
using UserManagement.Domain.Enums;
using FluentValidation;
using Common.DomainCommon.Validations;

namespace UserManagement.Domain.Validations;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Must(HaveFirstAndLastName)
            .WithMessage("The name must contain at least the first and last name.");

        RuleFor(user => user.Email)
            .SetValidator(new EmailValidator());

        RuleFor(user => user.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot be longer than 50 characters.");

        RuleFor(user => user.Password)
            .SetValidator(new PasswordValidator());

        RuleFor(user => user.Status)
            .NotEqual(UserStatus.Unknown)
            .WithMessage("User status cannot be Unknown.");

        RuleFor(user => user.Role)
            .NotEqual(UserRole.None)
            .WithMessage("User role cannot be None.");

        When(user => user.Phone is not null, () =>
        {
            RuleFor(user => user.Phone!.Value)
                .SetValidator(new PhoneValidator());
        });
    }

    private bool HaveFirstAndLastName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return false;

        var parts = name.Trim().Split(' ');
        return parts.Length >= 2;
    }
}
