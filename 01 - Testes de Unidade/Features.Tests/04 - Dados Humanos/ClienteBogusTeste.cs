using Xunit;

namespace Features.Tests;

[Collection(nameof(ClienteBogusCollection))]
public class ClienteBogusTeste
{
    private readonly ClienteBogusFixture _clientBogusCollection;

    public ClienteBogusTeste(ClienteBogusFixture clientBogusCollection)
    {
        _clientBogusCollection = clientBogusCollection;
    }

    [Fact(DisplayName = "Teste com o Bogus Cliente Valido")]
    [Trait("Categoria", "Bogus Teste")]
    public void Cliente_NovoCliente_DeveSerValido()
    {
        // Arrange
        var cliente = _clientBogusCollection.GerarClienteValido();

        // Act
        var result = cliente.EhValido();

        // Asset
        Assert.True(result);
    }

    [Fact(DisplayName = "Teste com o Cliente Invalido")]
    [Trait("Categoria", "Bogus Teste")]
    public void Cliente_NovoCliente_DeveSerInvalido()
    {
        // Arrange
        var cliente = _clientBogusCollection.GerarClienteInvalido();

        // Act
        var result = cliente.EhValido();

        // Asset
        Assert.False(result);
    }

}