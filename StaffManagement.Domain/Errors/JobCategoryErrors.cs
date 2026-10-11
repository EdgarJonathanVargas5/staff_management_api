using StaffManagement.Domain.Abstractions;

namespace StaffManagement.Domain.Errors;

public static class JobCategoryErrors
{
    public static Error NotFound = new Error(
        "JOB_CATEGORY.NOT_FOUND", 
        "No se ha podido encontrar la categoria de trabajo con el identificador proporcionado."
    );
}
