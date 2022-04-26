using System.Diagnostics;
using System.Linq;
using System.Threading;
using Features.Clientes;
using MediatR;
using Moq;
using Xunit;

namespace Features.Tests;

[Collection(nameof(ClienteBogusCollection))]
public class ClienteTests
{
    private readonly ClienteBogusFixture _clienteTestsFixture;

    public ClienteTests(ClienteBogusFixture clienteTestsFixture)
    {
        _clienteTestsFixture = clienteTestsFixture;
    }

    [Fact(DisplayName = "Adicionar Cliente com Sucesso")]
    [Trait("Categoria", "Cliente Service Mock Testes")]
    public void ClienteService_Adicionar_DeveExecutarComSucesso()
    {
        // Arrange
        var cliente = _clienteTestsFixture.GerarClienteValido();
        var clienteRepo = new Mock<IClienteRepository>();
        var mediaTr = new Mock<IMediator>();

        var clienteService = new ClienteService(clienteRepo.Object, mediaTr.Object);

        // Act
        clienteService.Adicionar(cliente);

        //Assert 
        clienteRepo.Verify(r => r.Adicionar(cliente), Times.Once);
        mediaTr.Verify(m=>m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Once);
    }

    [Fact(DisplayName = "Adicionar Cliente com Falha")]
    [Trait("Categoria", "Cliente Service Mock Testes")]
    public void ClienteService_Adicionar_DeveFalaharCadastroClienteInvalido()
    {
        // Arrange
        var cliente = _clienteTestsFixture.GerarClienteInvalido();
        var clienteRepo = new Mock<IClienteRepository>();
        var mediaTr = new Mock<IMediator>();

        var clienteService = new ClienteService(clienteRepo.Object, mediaTr.Object);

        // Act
        clienteService.Adicionar(cliente);

        //Assert 
        clienteRepo.Verify(r => r.Adicionar(cliente), Times.Never);
        mediaTr.Verify(m => m.Publish(It.IsAny<INotification>(), CancellationToken.None), Times.Never);
    }

    [Fact(DisplayName = "Buscar Clientes Ativos")]
    [Trait("Categoria", "Cliente Service Mock Testes")]
    public void ClienteService_ObterTodosAtivos_DeveRetornarApenasClientesAtivos()
    {
        // Arrange 
        var clienteRepo = new Mock<IClienteRepository>();
        var mediaTr = new Mock<IMediator>();

        clienteRepo.Setup(c => c.ObterTodos()).Returns(_clienteTestsFixture.GerarClientesVariados());

        var clienteService = new ClienteService(clienteRepo.Object, mediaTr.Object);

        // Act
        var clientes = clienteService.ObterTodosAtivos().ToList();

        // Assert
        clienteRepo.Verify(r => r.ObterTodos(), Times.Once);
        Assert.True(clientes.Any());
        Assert.All(clientes, cliente => Assert.True(cliente.Ativo));
    }



}