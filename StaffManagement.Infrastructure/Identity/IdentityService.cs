using Microsoft.AspNetCore.Identity;
using StaffManagement.Application.Abstractions;
using StaffManagement.Application.Authentication.Dtos;
using StaffManagement.Domain.Errors;

namespace StaffManagement.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public IdentityService(UserManager<ApplicationUser> userManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _userManager = userManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<Result<AuthUserResponse>> LoginAsync(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return Result<AuthUserResponse>.Failure(IdentityErrors.InvalidCredentials);
        }

        var isValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isValidPassword)
        {
            return Result<AuthUserResponse>.Failure(IdentityErrors.InvalidCredentials);
        }

        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email!, user.FullName, roles);

        var authResponse = new AuthUserResponse(
            user.Id, user.Email!, user.FullName, token
        );
        
        return Result<AuthUserResponse>.Success(authResponse);
    }

    public async Task<Result<AuthUserResponse>> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            return Result<AuthUserResponse>.Failure(IdentityErrors.UserAlreadyExists);
        }

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FullName = request.FullName
        };

        var creationResult = await _userManager.CreateAsync(user, request.Password);
        if (!creationResult.Succeeded)
        {
            var firstError = creationResult.Errors.FirstOrDefault()?.Description ?? "Error al crear usuario";
            return Result<AuthUserResponse>.Failure(IdentityErrors.RegisterFailed(firstError));
        }
        await _userManager.AddToRoleAsync(user, "User");
        var roles = await _userManager.GetRolesAsync(user);
        var token = _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.FullName, roles);
        
        var authResponse = new AuthUserResponse(
            user.Id, user.Email, user.FullName, token
        );
        
        return Result<AuthUserResponse>.Success(authResponse);
    }
}
