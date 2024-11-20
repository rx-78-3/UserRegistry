using FluentValidation;
using UserManagement.Application.Messages.Commands;

namespace UserManagement.Application.Messages.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.User.Email).NotEmpty();
        RuleFor(x => x.User.Password).NotEmpty();
        RuleFor(x => x.User.ProvinceId).NotEmpty();
    }
}
