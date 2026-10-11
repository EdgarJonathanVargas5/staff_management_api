namespace StaffManagement.Application.Authentication.Dtos;

public sealed record AuthUserResponse(
    string Id,
    string Email,
    string Password,
    string Token
);

