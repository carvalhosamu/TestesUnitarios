using System.Linq;
using System.Threading;
using Features.Clientes;
using FluentAssertions;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteFluentAssertionsTests
    {
        private readonly ClienteBogusFixture _clientBogusCollection;

        public ClienteFluentAssertionsTests(ClienteBogusFixture clientBogusCollection)
        {
            _clientBogusCollection = clientBogusCollection;
        }

        [Fact(DisplayName = "Teste com o Bogus Cliente Valido")]
        [Trait("Categoria", "FluentAssertions Cliente")]
        public void Cliente_NovoCliente_DeveSerValido()
        {
            // Arrange
            var cliente = _clientBogusCollection.GerarClienteValido();

            // Act
            var result = cliente.EhValido();

            // Assert
            /*Assert.True(result);
            Assert.Empty(cliente.ValidationResult.Errors);*/

            cliente.ValidationResult.Errors.Should().BeEmpty();
            result.Should().BeTrue();
        }

        [Fact(DisplayName = "Teste com o Cliente Invalido")]
        [Trait("Categoria", "FluentAssertions Cliente")]
        public void Cliente_NovoCliente_DeveSerInvalido()
        {
            // Arrange
            var cliente = _clientBogusCollection.GerarClienteInvalido();

            // Act
            var result = cliente.EhValido();

            // Asset
            result.Should().BeFalse();
            cliente.ValidationResult.Errors.Should().HaveCountGreaterThanOrEqualTo(1, "Deve possuir erros de validação");
        }
    }
}
