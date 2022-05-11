using Xunit;

namespace Demo.Tests
{
    public class AssertStringTests
    {
        [Fact]
        public void StringTools_UnirNomes_RetornarCompleto()
        {
            // Arrange
            var stringTools = new StringsTools();

            // Act
            var nomeCompleto = stringTools.Unir("Samuel", "Carvalho");

            // Assert
            Assert.Equal("Samuel Carvalho", nomeCompleto);

        }

        [Fact]
        public void StringTools_UnirNomes_RetornarIgnorarCase()
        {
            // Arrange
            var stringTools = new StringsTools();

            // Act
            var nomeCompleto = stringTools.Unir("Samuel", "Carvalho");

            // Assert
            Assert.Equal("SAMUEL CARVALHO", nomeCompleto, true);

        }
        
        [Fact]
        public void StringTools_UnirNomes_DeveConterTrecho()
        {
            // Arrange
            var stringTools = new StringsTools();

            // Act
            var nomeCompleto = stringTools.Unir("Samuel", "Carvalho");

            // Assert
            Assert.Contains("muel", nomeCompleto);

        }

        [Fact]
        public void StringTools_UnirNomes_DeveComecarCom()
        {
            // Arrange
            var stringTools = new StringsTools();

            // Act
            var nomeCompleto = stringTools.Unir("Samuel", "Carvalho");

            // Assert
            Assert.StartsWith("Sam", nomeCompleto);

        }

        [Fact]
        public void StringTools_UnirNomes_DeveAcabarcom()
        {
            // Arrange
            var stringTools = new StringsTools();

            // Act
            var nomeCompleto = stringTools.Unir("Samuel", "Carvalho");

            // Assert
            Assert.EndsWith("valho", nomeCompleto);

        }

        [Fact]
        public void StringTools_UnirNomes_ValidarExpressaoRegular()
        {
            // Arrange
            var stringTools = new StringsTools();

            // Act
            var nomeCompleto = stringTools.Unir("Samuel", "Carvalho");

            // Assert
            Assert.Matches("[A-Z]{1}[a-z]+ [A-Z]{1}[a-z]+", nomeCompleto);
        }

    }
}
