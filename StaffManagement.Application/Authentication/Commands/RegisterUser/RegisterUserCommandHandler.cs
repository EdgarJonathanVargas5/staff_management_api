using StaffManagement.Application.Authentication.Dtos;

namespace StaffManagement.Application.Authentication.Commands.RegisterUser;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<AuthUserResponse>>
{
    private readonly IIdentityService _identityService;

    public RegisterUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Result<AuthUserResponse>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.RegisterAsync(request.Request, cancellationToken);
        return response;
    }
}
