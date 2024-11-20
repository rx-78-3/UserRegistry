using BuildingBlocks.Cqrs;
using UserManagement.Application.Dtos;

namespace UserManagement.Application.Messages.Commands;

public record CreateUserCommand(CreateUserDto User) : ICommand<CreateUserResult>;
