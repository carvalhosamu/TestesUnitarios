using Xunit;

namespace Features.Tests;


[Collection(nameof(ClientCollection))]
public class ClienteTesteValido
{

    private readonly ClienteTestsFixture _clientTextFixture;

    public ClienteTesteValido(ClienteTestsFixture clientTextFixture)
    {
        _clientTextFixture = clientTextFixture;
    }

    [Fact(DisplayName = "Novo Cliente Válido")]
    [Trait("Categoria", "Categoria Client Trait Testes")]
    public void Cliente_NovoCliente_DeveEstarValido()
    {
        // arrange
        var cliente = _clientTextFixture.GerarClienteValido();

        // Act
        var result = cliente.EhValido();

        // Assert
        Assert.True(result);
        Assert.Empty(cliente.ValidationResult.Errors);
    }
}