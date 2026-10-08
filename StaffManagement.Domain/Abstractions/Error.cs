namespace StaffManagement.Domain.Abstractions;

public record Error (
    string Code,
    string Message
);
