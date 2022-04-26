using Xunit;

namespace Features.Tests;

[Collection(nameof(ClientCollection))]
public class ClienteTesteInvalido
{
    private readonly ClienteTestsFixture _clienteTextFixture;

    public ClienteTesteInvalido(ClienteTestsFixture clienteTextFixture)
    {
        _clienteTextFixture = clienteTextFixture;
    }

    [Fact(DisplayName = "Novo Cliente Inválido")]
    [Trait("Categoria", "Categoria Client Trait Testes")]
    public void Cliente_NovoCliente_DeveEstarInvalido()
    {
        // Arrange
        var cliente = _clienteTextFixture.GerarClienteInvalido();

        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.False(result);
        Assert.NotEmpty(cliente.ValidationResult.Errors);
    }

}

