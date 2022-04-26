using Xunit;

namespace Demo.Tests;

public class AssertCollectionTests
{
    [Fact]
    public void Funcionario_Habilidades_NaoDevePossuirHabilidadesVazias()
    {
        //Arrange & Act
        var funcionario = new Funcionario("Samuel", 10000);

        //Assert
        Assert.All(funcionario.Habilidades, habilidade => Assert.False(string.IsNullOrEmpty(habilidade)));
    }

    [Fact]
    public void Funcionario_Habilidade_JuniorDevePossuirHabilidadesBasicas()
    {
        //Arrange & Act
        var funcionario = new Funcionario("Samuel", 1000);

        //Assert
        Assert.Contains("OOP", funcionario.Habilidades);

    }

    [Fact]
    public void Funcionario_Habilidade_JuniorNaoDevePossuirTodasHabilidadesBasicas()
    {
        //Arrange & Act
        var funcionario = new Funcionario("Samuel", 1000);

        //Assert
        Assert.DoesNotContain("Microsservices", funcionario.Habilidades);

    }
    [Fact]
    public void Funcionario_Habilidade_SeniorDevePossuirTodasHabilidadesBasicas()
    {
        //Arrange & Act
        var funcionario = new Funcionario("Samuel", 10000);

        var habilidades = new[]
        {
            "Lógica de Programação",
            "OOP",
            "Testes",
            "Microservices"
        };
    
        //Assert
        Assert.Equal(habilidades, funcionario.Habilidades);

    }
}