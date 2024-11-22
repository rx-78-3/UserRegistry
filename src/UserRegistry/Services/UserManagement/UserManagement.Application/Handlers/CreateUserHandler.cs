using System.Text.RegularExpressions;
using BuildingBlocks.Cqrs;
using BuildingBlocks.Cryptography.Abstractions;
using Domain.Base;
using UserManagement.Application.Abstractions;
using UserManagement.Application.Dtos;
using UserManagement.Application.Messages.Commands;
using UserManagement.Domain.Models;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Application.Handlers;

public class CreateUserHandler(IUserService userService, ISaltGenerator saltGenerator, IPasswordHasher passwordHasher)
    : ICommandHandler<CreateUserCommand, CreateUserResult>
{
    private static readonly Regex PasswordRegex = new(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$", RegexOptions.Compiled);

    public async Task<CreateUserResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = CreateNewUser(request.User);

        var location = await userService.GetAssociatedLocation(newUser, cancellationToken);

        if (location == null)
        {
            throw new KeyNotFoundException($"Province with ID '{newUser.Location.Province.Id}' does not exist.");
        }

        if (location.Country.Id != request.User.CountryId)
        {
            throw new DomainException("Country and province do not match.");
        }

        await userService.Create(newUser, cancellationToken);

        return new CreateUserResult(newUser.Id.Value);
    }

    private User CreateNewUser(CreateUserDto userDto)
    {
        var saltString = saltGenerator.GenerateString();

        if (!PasswordRegex.IsMatch(userDto.Password))
        {
            throw new DomainException("Invalid password");
        }

        var passwordHash = passwordHasher.ComputeHash(userDto.Password, saltString);

        var newUser = User.Create(
            UserId.Of(Guid.NewGuid()),
            Email.Of(userDto.Email),
            PasswordHash.Of(passwordHash),
            UserLocation.Of(userDto.CountryId, userDto.ProvinceId)
        );

        return newUser;
    }
}
