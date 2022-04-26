using System;
using Xunit;

namespace Demo.Tests;

public class AssertExceptionsTests
{
    [Fact]
    public void Calculadora_Dividir_DeveRetornarErroDivisaoPorZero()
    {
        
        // Arrange
        var calculadora = new Calculadora();

        //Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculadora.Dividir(0, 0));

    }

    [Fact]
    public void Funcionario_Salario_DeveRetornarInferiorPermitido()
    {
        //Arrange Act & Assert
        var exception = Assert.Throws<Exception>(() => new Funcionario("Samuel", 250));

        Assert.Equal("Salario inferior ao permitido", exception.Message);

    }

}