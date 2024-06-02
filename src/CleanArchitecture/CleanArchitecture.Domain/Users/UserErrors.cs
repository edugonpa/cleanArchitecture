namespace CleanArchitecture.Domain.Abstractions;

public static class UserErrors
{
    public static Error NotFound = new Error(
        "User.Found",
        "No existe el usuario buscado por este id"
    );

    public static Error InvalidCredentials = new Error(
        "User.InvalidCredentials",
        "Las credenciales son incorrectas"
    );

    public static Error AlreadyExists = new Error(
        "User.AlreadyExists",
        "El usuario ya existe en la base de datos"
    );
}