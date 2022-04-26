using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Demo.Tests
{
    public class AssertNullBoolTests
    {
        [Fact]
        public void Funcionario_Nome_NaoDeveSerNuloOuVazio()
        {
            // Arrange & Act
            var funcionario = new Funcionario("",1000);
            
            // Assert
            Assert.False(string.IsNullOrEmpty(funcionario.Nome));
        }

        [Fact]
        public void Funcionario_Nome_ApelidoDeveEstarVazio()
        {
            // Arrange & Act
            var funcionario = new Funcionario("", 1000);

            // Assert
            Assert.Null(funcionario.Apelido);

            // Assert not null
            Assert.True(string.IsNullOrEmpty(funcionario.Apelido));
            Assert.False(funcionario.Apelido?.Substring(1,2).Length > 0);

        }


    }
}
