using System;
using Features.Clientes;
using Xunit;

namespace Features.Tests;

public class ClientTests
{
    [Fact(DisplayName = "Novo Cliente Válido")]
    [Trait("Categoria", "Categoria Client Trait Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // Arrange
        var cliente = new Cliente(
            Guid.NewGuid(),
            "Samuel",
            "Carvalho",
            new DateTime(1996, 4, 4),
            "samuel96carvalho@email.com",
            true,
            DateTime.Now
        );

        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.True(result);
        Assert.Empty(cliente.ValidationResult.Errors);
    }

    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Categoria Client Trait Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = new Cliente(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now,
            "samuel2email.com",
            true,
            DateTime.Now
        );

        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.False(result);
        Assert.NotEmpty(cliente.ValidationResult.Errors);
    }
}