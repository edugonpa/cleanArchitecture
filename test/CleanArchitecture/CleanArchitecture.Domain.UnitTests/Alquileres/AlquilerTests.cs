using CleanArchitecture.Domain.Alquileres;
using CleanArchitecture.Domain.Alquileres.Events;
using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.UnitTest.Infrastructure;
using CleanArchitecture.Domain.UnitTest.Users;
using CleanArchitecture.Domain.UnitTest.Vehiculos;
using CleanArchitecture.Domain.Users;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Domain.UnitTest.Alquileres;

public class AlquilerTest : BaseTest
{
    [Fact]
    public void Reserva_Should_RaiseAlquilerReservaDomainEvent()
    {
        // Arrange
        var user = User.Create(
            userMock.Nombre,
            userMock.Apellido,
            userMock.Email,
            userMock.Password
        );

        var precio = new Moneda(10.0m, TipoMoneda.Usd);

        var duration = DateRange.Create(
            new DateOnly(2024,1,1),
            new DateOnly(2025,1,1)
        );

        var vehiculo = VehiculoMock.Create(precio);
        var precioService = new PrecioService();

        // Act

        var alquiler = Alquiler.Reservar(
            vehiculo,
            user.Id!,
            duration,
            DateTime.UtcNow,
            precioService
        );
    
        // Assert
        var alquilerReservaDomainEvent =
        AssertDomainEventWasPublished<AlquilerReservadoDomainEvent>(alquiler);

        alquilerReservaDomainEvent.AlquilerId.Should().Be(alquiler.Id);
    }
}