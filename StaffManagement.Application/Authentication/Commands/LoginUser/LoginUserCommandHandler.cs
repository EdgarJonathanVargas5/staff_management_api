using StaffManagement.Application.Authentication.Dtos;

namespace StaffManagement.Application.Authentication.Commands.LoginUser;

public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<AuthUserResponse>>
{
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<AuthUserResponse>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.LoginAsync(request.Request, cancellationToken);
        return response;
    }
}
