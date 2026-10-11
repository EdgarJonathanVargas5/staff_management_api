using StaffManagement.Application.Authentication.Dtos;

namespace StaffManagement.Application.Abstractions;

public interface IIdentityService
{
    Task<Result<AuthUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken);
    Task<Result<AuthUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken);
}
