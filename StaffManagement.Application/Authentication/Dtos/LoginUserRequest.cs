namespace StaffManagement.Application.Authentication.Dtos;

public sealed record LoginUserRequest(
    string Email,
    string Password
);
