using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{

    public class CalculadoraTests
    {
        [Fact]
        public void Calculadora_Somar_RetornarValorSoma()
        {
            //Arrange
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Somar(2, 2);

            //Assert

            Assert.Equal(4, resultado);
        }

        [Theory]
        [InlineData(1, 1, 2)]
        [InlineData(1,2,3)]
        [InlineData(7,3,10)]
        [InlineData(14,14,28)]
        [InlineData(6,6,12)]
        [InlineData(9,9,18)]
        public void Calculadora_Somar_RetornarValoresSomaCorretos(double v1,double v2,double total)
        {
            var calculadora = new Calculadora();

            //Act
            var resultado = calculadora.Somar(v1, v2);

            //Assert

            Assert.Equal(total, resultado);
        }
    }
}
