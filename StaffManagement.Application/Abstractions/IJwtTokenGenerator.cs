namespace StaffManagement.Application.Abstractions;

public interface IJwtTokenGenerator
{
    string GenerateToken(string userId, string email, string fullName, IEnumerable<string> roles);
}
