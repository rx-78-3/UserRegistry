using System.Text.RegularExpressions;
using BuildingBlocks.Cqrs;
using CoreServices.Cryptography.Abstractions;
using Domain.Base;
using UserManagement.Application.DataAccess;
using UserManagement.Application.Dtos;
using UserManagement.Application.Messages.Commands;
using UserManagement.Domain.Models;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Application.Handlers;

public class CreateUserHandler(IUserRepository userRepository, ISaltGenerator saltGenerator, IPasswordHasher passwordHasher)
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    private static readonly Regex EmailRegex = new(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.Compiled);

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        // TODO add location fields validation with requesting cached LocationService here.

        var newUser = CreateNewUser(request.User);

        await userRepository.Create(newUser, cancellationToken);

        return new CreateUserResult(newUser.Id.Value);
    }

    private User CreateNewUser(CreateUserDto userDto)
    {
        var saltString = saltGenerator.GenerateString();

        if (!EmailRegex.IsMatch(userDto.Password))
        {
            throw new DomainException("Invalid password");
        }

        var passwordHash = passwordHasher.ComputeHash(userDto.Password, saltString);

        var newUser = User.Create(
            UserId.Of(Guid.NewGuid()),
            Email.Of(userDto.Email),
            PasswordHash.Of(passwordHash),
            ProvinceId.Of(userDto.ProvinceId)
        );

        return newUser;
    }
}
