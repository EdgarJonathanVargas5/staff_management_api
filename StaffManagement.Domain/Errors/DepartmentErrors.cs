using StaffManagement.Domain.Abstractions;

namespace StaffManagement.Domain.Errors;

public static class DepartmentErrors
{
    public static Error NotFound = new Error(
        "DEPARTMENT.NOT_FOUND", 
        "No se ha podido encontrar un departamento con el identificador proporcionado."
    );

    public static Error DuplicatedName = new Error(
        "DEPARTMENT.DUPLICATED_NAME", 
        "El nombre del departamento ingresado ya se encuentra registrado en el sistema."
    );

    public static Error FailUpdate = new Error(
        "DEPARTMENT.FAIL_UPDATE", 
        "Ocurrió un error al intentar actualizar los datos del departamento."
    );

    public static Error FailDelete = new Error(
        "DEPARTMENT.FAIL_DELETE", 
        "No se ha podido eliminar el departamento."
    );

    public static Error FailCreate = new Error(
        "DEPARTMENT.FAIL_CREATE", 
        "No se ha podido crear el departamento."
    );
}
