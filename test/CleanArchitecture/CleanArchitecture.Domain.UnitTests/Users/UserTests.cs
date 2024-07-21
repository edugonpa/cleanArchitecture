using CleanArchitecture.Domain.Roles;
using CleanArchitecture.Domain.UnitTest.Infrastructure;
using CleanArchitecture.Domain.Users;
using CleanArchitecture.Domain.Users.Events;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTest.Users;

public class UserTests : BaseTest
{
    [Fact]
    public void Create_Should_SetPropertyValues()
    {
        //Arrange  --> Vamos a crear un Mock File -> userMock

        //Act

        var user = User.Create(
            userMock.Nombre, 
            userMock.Apellido,
            userMock.Email,
            userMock.Password
            );

        //Assert

        user.Nombre.Should().Be(userMock.Nombre);
        user.Apellido.Should().Be(userMock.Apellido);
        user.Email.Should().Be(userMock.Email);
        user.PasswordHash.Should().Be(userMock.Password);
    }

    [Fact]
    public void Create_Should_RaiseUserCreateDomainEvent()
    {
        var user = User.Create(
            userMock.Nombre,
            userMock.Apellido,
            userMock.Email,
            userMock.Password
        );

        // var domainEvent= user.GetDomainEvents().OfType<UserCreatedDomainEvent>().SingleOrDefault();
        var domainEvent = AssertDomainEventWasPublished<UserCreatedDomainEvent>(user);

        domainEvent!.UserId.Should().Be(user.Id);
    }

    [Fact]
    public void Create_Should_AddRegisterRoleToUser()
    {
        var user = User.Create(
            userMock.Nombre,
            userMock.Apellido,
            userMock.Email,
            userMock.Password
        );

        user.Roles.Should().Contain(Role.Cliente);
    }

}