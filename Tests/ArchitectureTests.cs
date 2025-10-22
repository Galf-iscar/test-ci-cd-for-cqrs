using NetArchTest.Rules;
using Xunit;

namespace CQRS_App.Tests;

public class ArchitectureTests
{
    # region enforce layering restrictions

    [Fact]
    public void Domain_Should_Not_HaveDependencyOnInfrastructure()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.Domain.Models.Employee).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.Infrastructure")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "Domain layer should not depend on Infrastructure layer.");
    }
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnApplication()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.Domain.Models.Employee).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.Application")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "Domain layer should not depend on Application layer.");
    }
    [Fact]
    public void Domain_Should_Not_HaveDependencyOnAPI()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.Domain.Models.Employee).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.API")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "Domain layer should not depend on API layer.");
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnInfrastructure()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.Application.Registrations.Registrations).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.Infrastructure")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "Application layer should not depend on Infrastructure layer.");
    }

    [Fact]
    public void Application_Should_Not_HaveDependencyOnAPI()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.Application.Registrations.Registrations).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.API")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "Application layer should not depend on API layer.");
    }


    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnPresentation()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.Infrastructure.Repositories.EmployeeRepository).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.API")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "Infrastructure layer should not depend on Presentation layer.");
    }

    [Fact]
    public void Infrastructure_Should_Not_HaveDependencyOnApplication()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.Infrastructure.Repositories.EmployeeRepository).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.Application")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "Infrastructure layer should not depend on Application layer.");
    }

    [Fact]
    public void API_Should_Not_HaveDependencyOnDomain()
    {
        // Arrange & Act
        var result = Types.InAssembly(typeof(CQRS_App.API.Controllers.EmployeesController).Assembly)
            .ShouldNot()
            .HaveDependencyOn("CQRS_App.Domain")
            .GetResult();

        // Assert
        Assert.True(result.IsSuccessful, "API layer should not depend on domain layer.");
    }

    #endregion
}