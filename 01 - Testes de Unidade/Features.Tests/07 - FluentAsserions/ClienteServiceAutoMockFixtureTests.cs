using System.Linq;
using System.Threading;
using Features.Clientes;
using FluentAssertions;
using FluentAssertions.Extensions;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteAutoMockerCollection))]
    public class ClienteServiceFluentAssertionsTestes
    {
        private readonly ClienteTestsAutoMockFixture _clienteTestsFixture;
        private readonly ClienteService _clienteService;

        public ClienteServiceFluentAssertionsTestes(ClienteTestsAutoMockFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
            _clienteService = _clienteTestsFixture.ObterClienteService();
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service FluentAssertions Testes")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            // Act
            _clienteService.Adicionar(cliente);

            //Assert 
            cliente.EhValido().Should().BeTrue();
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            _clienteTestsFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service FluentAssertions Testes")]
        public void ClienteService_Adicionar_DeveFalaharCadastroClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();
 
            // Act
            _clienteService.Adicionar(cliente);

            //Assert 

            cliente.EhValido().Should().BeFalse();  
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            _clienteTestsFixture.Mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Buscar Clientes Ativos")]
        [Trait("Categoria", "Cliente Service FluentAssertions Testes")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange 
            
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos()).Returns(_clienteTestsFixture.GerarClientesVariados());

            // Act
            var clientes = _clienteService.ObterTodosAtivos().ToList();

            // Assert
            _clienteTestsFixture.Mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            clientes.Should().HaveCountGreaterThanOrEqualTo(1);
            clientes.Should().NotContain(c => !c.Ativo);

            _clienteService.ExecutionTimeOf(c => c.ObterTodosAtivos()).Should().BeLessThanOrEqualTo(50.Milliseconds());
        }
    }
}
