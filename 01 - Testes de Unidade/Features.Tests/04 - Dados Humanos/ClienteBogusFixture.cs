using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using Features.Clientes;
using Xunit;

namespace Features.Tests;

[CollectionDefinition(nameof(ClienteBogusCollection))]
public class ClienteBogusCollection : ICollectionFixture<ClienteBogusFixture> { }

public class ClienteBogusFixture
{
    private readonly Faker<Cliente> _clienteFaker;

    public ClienteBogusFixture()
    {
        _clienteFaker = new Faker<Cliente>("pt_BR");
    }

    public Cliente GerarClienteValido()
    {
        return GerarClientes().FirstOrDefault()!;
        
    }

    public Cliente GerarClienteInvalido()
    {
        var genero = new Faker().PickRandom<Name.Gender>();

        Cliente cliente = _clienteFaker.CustomInstantiator(faker => new Cliente(
            Guid.NewGuid(),
            faker.Name.FirstName(genero),
            faker.Name.LastName(genero),
            faker.Date.Past(17, DateTime.Now),
            string.Empty,
            false,
            DateTime.Now
        ));

        return cliente;
    }

    public IEnumerable<Cliente> GerarClientes(int quantidade = 1, bool ativo = true)
    {
        var genero = new Faker().PickRandom<Name.Gender>();

        Cliente cliente = _clienteFaker.CustomInstantiator(faker => new Cliente(
            Guid.NewGuid(),
            faker.Name.FirstName(genero),
            faker.Name.LastName(genero),
            faker.Date.Past(80, DateTime.Now.AddYears(-18)),
            string.Empty,
            ativo,
            DateTime.Now
        )).RuleFor(c => c.Email, (faker, cliente) => faker.Internet.Email(cliente.Nome, cliente.Sobrenome));

        return _clienteFaker.Generate(quantidade);
    }

    public IEnumerable<Cliente> GerarClientesVariados()
    {
        var clientes = new List<Cliente>();

        clientes.AddRange(GerarClientes(50));
        clientes.AddRange(GerarClientes(50, false));

        return clientes;
    }



    public void Dispose()
    {

    }


}