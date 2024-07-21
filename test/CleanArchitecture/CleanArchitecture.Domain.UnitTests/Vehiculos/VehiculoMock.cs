using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.UnitTest.Vehiculos;

internal static class VehiculoMock
{
    public static Vehiculo Create(Moneda precio, Moneda? mantenimiento=null)
    => new(
        VehiculoId.New(),
        new Modelo("Civi"),
        new Vin("45dsfdsd5444"),
        precio,
        mantenimiento ?? Moneda.Zero(),
        DateTime.UtcNow.AddYears(-1),
        [],
        new Direccion("USA", "Texas", "Lorenz", "El Paso", "Av. El Alamo")
    );
}