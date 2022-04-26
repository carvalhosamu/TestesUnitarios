using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class AssertNumbersTests
    {
        [Fact]
        public void Calculador_Somar_DeveSerIgual()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Somar(1, 2);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public void Calculador_Somar_DeveSerDiferente()
        {
            // Arrange
            var calculadora = new Calculadora();

            // Act
            var result = calculadora.Somar(1.199999999, 2.800000002);

            // Assert
            Assert.NotEqual(4, result, 10);
        }
    }
}
