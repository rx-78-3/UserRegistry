using UserManagement.Application.Dtos;

namespace UserManagement.Api.EndpointMethods.PostUser;

public record PostUserRequest(CreateUserDto User);
