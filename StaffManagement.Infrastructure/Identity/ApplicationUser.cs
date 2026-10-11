using Microsoft.AspNetCore.Identity;

namespace StaffManagement.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
