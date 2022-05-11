using System.Linq;
using System.Threading;
using Features.Clientes;
using MediatR;
using Moq;
using Moq.AutoMock;
using Xunit;

namespace Features.Tests
{
    [Collection(nameof(ClienteBogusCollection))]
    public class ClienteServiceAutoMockTests
    {
        private readonly ClienteBogusFixture _clienteTestsFixture;

        public ClienteServiceAutoMockTests(ClienteBogusFixture clienteTestsFixture)
        {
            _clienteTestsFixture = clienteTestsFixture;
        }

        [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Testes")]
        public void ClienteService_Adicionar_DeveExecutarComSucesso()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteValido();

            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            // Act
            clienteService.Adicionar(cliente);

            //Assert 
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Once);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
        }

        [Fact(DisplayName = "Adicionar Cliente com Falha")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Testes")]
        public void ClienteService_Adicionar_DeveFalaharCadastroClienteInvalido()
        {
            // Arrange
            var cliente = _clienteTestsFixture.GerarClienteInvalido();
            
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();
 
            // Act
            clienteService.Adicionar(cliente);

            // Assert 
            mocker.GetMock<IClienteRepository>().Verify(r => r.Adicionar(cliente), Times.Never);
            mocker.GetMock<IMediator>().Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
        }

        [Fact(DisplayName = "Buscar Clientes Ativos")]
        [Trait("Categoria", "Cliente Service AutoMockFixture Testes")]
        public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
        {
            // Arrange 
            var mocker = new AutoMocker();
            var clienteService = mocker.CreateInstance<ClienteService>();

            mocker.GetMock<IClienteRepository>().Setup(c => c.ObterTodos()).Returns(_clienteTestsFixture.GerarClientesVariados());

            // Act
            var clientes = clienteService.ObterTodosAtivos().ToList();

            // Assert
            mocker.GetMock<IClienteRepository>().Verify(r => r.ObterTodos(), Times.Once);
            Assert.True(clientes.Any());
            Assert.All(clientes, cliente => Assert.True(cliente.Ativo));
        }
    }
}
