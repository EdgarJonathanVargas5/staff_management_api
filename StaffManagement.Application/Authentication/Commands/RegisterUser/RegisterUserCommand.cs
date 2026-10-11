using StaffManagement.Application.Authentication.Dtos;

namespace StaffManagement.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommand : IRequest<Result<AuthUserResponse>>
{
    public required RegisterUserRequest Request { get; set; }
}
