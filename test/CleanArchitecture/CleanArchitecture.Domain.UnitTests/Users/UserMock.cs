using CleanArchitecture.Domain.Users;

namespace CleanArchitecture.Domain.UnitTest.Users;

internal class userMock{
    public static readonly Nombre Nombre = new Nombre("Alfonso");
    public static readonly Apellido Apellido = new Apellido("Ramos");
    public static readonly Email Email = new Email("alfonso.ramos@gmail.com");
    public static readonly PasswordHash Password = new ("Test234Test4%");

}