using StaffManagement.Domain.Abstractions;

namespace StaffManagement.Domain.Errors;

public static class IdentityErrors
{
    public static Error InvalidCredentials = new Error(
        "IDENTITY.INVALID_CREDENTIALS", "Credenciales incorrectas"
    );

    public static Error UserAlreadyExists = new Error(
        "IDENTITY.USER_ALREADY_EXISTS", "El usuario ya está registrado"
    );
    public static Error RegisterFailed(string error) => new Error("IDENTITY.REGISTER_FAILED", error);
}
