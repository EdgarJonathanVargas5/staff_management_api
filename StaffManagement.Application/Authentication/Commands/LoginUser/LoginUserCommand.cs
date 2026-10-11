using StaffManagement.Application.Authentication.Dtos;

namespace StaffManagement.Application.Authentication.Commands.LoginUser;

public class LoginUserCommand : IRequest<Result<AuthUserResponse>>
{
    public required LoginUserRequest Request { get; set; }
}
