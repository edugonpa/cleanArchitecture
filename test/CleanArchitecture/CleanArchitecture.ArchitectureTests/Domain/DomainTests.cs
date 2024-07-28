using System.Reflection;
using CleanArchitecture.ArchitectureTests.Infrastructure;
using CleanArchitecture.Domain.Abstractions;
using FluentAssertions;
using NetArchTest.Rules;
using Xunit;

namespace CleanArchitecture.ArchitectureTests.Domain;

public class DomainTests : BaseTest
{
    [Fact]
    public void Entities_ShouldHave_PrivateConstructorNoParameters()
    {
        // Given
        IEnumerable<Type> entityTypes = Types.InAssembly(DomainAssembly)
            .That()
            .Inherit(typeof(Entity<>))
            .GetTypes();

        var errorEntities = new List<Type>();

        foreach (Type entityType in entityTypes)
        {
            ConstructorInfo[] constructores = entityType.GetConstructors(
                BindingFlags.NonPublic | 
                BindingFlags.Instance
            );

            if(!constructores.Any(c=> c.IsPrivate && c.GetParameters().Length == 0))
            {
                errorEntities.Add(entityType);
            }
        }

        errorEntities.Should().BeEmpty();
        // When
    
        // Then
    }
}