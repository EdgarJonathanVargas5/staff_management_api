using StaffManagement.Domain.Abstractions;

namespace StaffManagement.Domain.Errors;

public static class EmployeeErrors
{
    public static Error NotFound = new Error(
        "EMPLOYEE.NOT_FOUND", 
        "No se ha podido encontrar un empleado con el identificador proporcionado."
    );

    public static Error DuplicatedEmail = new Error(
        "EMPLOYEE.DUPLICATED_EMAIL", 
        "El correo electrónico ingresado ya pertenece a otro empleado registrado."
    );

    public static Error DuplicatedTaxId = new Error(
        "EMPLOYEE.DUPLICATED_TAXID", 
        "El taxId ingresado ya se encuentra registrado en el sistema."
    );

    public static Error FailUpdate = new Error(
        "EMPLOYEE.FAIL_UPDATE", 
        "Ocurrió un error al intentar actualizar los datos del empleado."
    );

    public static Error FailDelete = new Error(
        "EMPLOYEE.FAIL_DELETE", 
        "No se ha podido eliminar el registro del empleado."
    );
}
