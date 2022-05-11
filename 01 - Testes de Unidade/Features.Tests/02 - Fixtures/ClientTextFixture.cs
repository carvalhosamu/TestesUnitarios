using System;
using Features.Clientes;
using Xunit;

namespace Features.Tests;

[CollectionDefinition(nameof(ClientCollection))]
public class ClientCollection : ICollectionFixture<ClienteTestsFixture> { }

public class ClienteTestsFixture : IDisposable
{
    public Cliente GerarClienteValido()
    {

        return new Cliente(
            Guid.NewGuid(),
            "Samuel",
            "Carvalho",
            new DateTime(1996, 4, 4),
            "samuel96carvalho@email.com",
            true,
            DateTime.Now
        );
    }

    public Cliente GerarClienteInvalido()
    {
        return new Cliente(
            Guid.NewGuid(),
            "",
            "",
            DateTime.Now,
            "samuel2email.com",
            true,
            DateTime.Now
        );
    }

    public void Dispose()
    {
    }
}