namespace StaffManagement.Application.Authentication.Dtos;

public sealed record RegisterUserRequest(
    string Email,
    string Password,
    string FullName
);

