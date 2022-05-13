using System.Linq;
using System.Threading;
using Features.Clientes;
using FluentAssertions;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests
{
    public class ClienteSkipTests
    {
        private readonly ClienteBogusFixture _clientBogusCollection;

        public ClienteSkipTests(ClienteBogusFixture clientBogusCollection)
        {
            _clientBogusCollection = clientBogusCollection;
        }

        [Fact(DisplayName = "Teste com  skip Cliente Valido", Skip = "Nova versão quebrando")]
        [Trait("Categoria", "Teste com skip")]
        public void Cliente_NovoCliente_DeveSerValido()
        {
            // Arrange
            var cliente = _clientBogusCollection.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Asset
            result.Should().BeFalse();
        }


    }
}