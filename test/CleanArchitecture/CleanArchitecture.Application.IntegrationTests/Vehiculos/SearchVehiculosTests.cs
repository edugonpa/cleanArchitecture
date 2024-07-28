using CleanArchitecture.Application.Vehiculos.SearchVehiculos;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Application.IntegrationTests.Vehiculos;

public class SearchVehiculos : BaseIntegrationTest
{
    public SearchVehiculos(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task SearchVehiculos_ShouldReturnEmptyList_WhenDateRangeInvalid()
    {
        // Arrange
        var query = new SearchVehiculosQuery(
            new DateOnly(2023,1,1),
            new DateOnly(2022,1,1)
        );

        // Act

        var resultado = await Sender.Send(query);
    
        // Assert

        resultado.Value.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchVehiculos_ShouldReturnEmptyList_WhenDateRangeIsInvalid()
    {
        // Arrange
        var query = new SearchVehiculosQuery(
            new DateOnly(2023,1,1),
            new DateOnly(2026,1,1)
        );

        // Act

        var resultado = await Sender.Send(query);
    
        // Assert

        resultado.IsSuccess.Should().BeTrue();
    }
}