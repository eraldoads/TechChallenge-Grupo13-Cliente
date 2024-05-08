using Domain.ValueObjects;
using Xunit;

namespace Domain.Tests.ValueObjects
{
    public class CPFAttributeTests
    {
        [Trait("Categoria", "ValueObjects")]
        [Theory]
        [InlineData("12345678900")]
        [InlineData("12345678901")]
        [InlineData("12345678919")]
        public void IsValid_InvalidCPF_ReturnsFalse(string cpf)
        {
            // Arrange
            var cpfAttribute = new CPFAttribute();

            // Act
            var isValid = cpfAttribute.IsValid(cpf);

            // Assert
            Assert.False(isValid);
        }

        [Trait("Categoria", "ValueObjects")]
        [Theory]
        [InlineData("00000000000")]
        [InlineData("11111111111")]
        [InlineData("22222222222")]
        public void IsValid_ValidCPF_ReturnsTrue(string cpf)
        {
            // Arrange
            var cpfAttribute = new CPFAttribute();

            // Act
            var isValid = cpfAttribute.IsValid(cpf);

            // Assert
            Assert.True(isValid);
        }

        [Trait("Categoria", "ValueObjects")]
        [Fact(DisplayName = "Retorna True para CPF Nulo")]
        public void IsValid_NullCPF_ReturnsTrue()
        {
            // Arrange
            var cpfAttribute = new CPFAttribute();

            // Act
            var isValid = cpfAttribute.IsValid(null);

            // Assert
            Assert.True(isValid);
        }

        [Trait("Categoria", "ValueObjects")]
        [Fact(DisplayName = "Retorna True para CPF Vazio")]
        public void IsValid_EmptyCPF_ReturnsTrue()
        {
            // Arrange
            var cpfAttribute = new CPFAttribute();

            // Act
            var isValid = cpfAttribute.IsValid("");

            // Assert
            Assert.True(isValid);
        }

        [Trait("Categoria", "ValueObjects")]
        [Fact(DisplayName = "Retorna False para CPF com Espaços")]
        public void IsValid_CPFWithSpaces_ReturnsTrue()
        {
            // Arrange
            var cpfAttribute = new CPFAttribute();

            // Act
            var isValid = cpfAttribute.IsValid("123 456 789 00");

            // Assert
            Assert.False(isValid);
        }

        [Trait("Categoria", "ValueObjects")]
        [Fact(DisplayName = "Retorna False para CPF com Caracteres Especiais")]
        public void IsValid_CPFWithSpecialCharacters_ReturnsTrue()
        {
            // Arrange
            var cpfAttribute = new CPFAttribute();

            // Act
            var isValid = cpfAttribute.IsValid("123.456.789-00");

            // Assert
            Assert.False(isValid);
        }

        [Trait("Categoria", "ValueObjects")]
        [Fact(DisplayName = "Retorna False para CPF com Letras")]
        public void IsValid_CPFWithLetters_ReturnsTrue()
        {
            // Arrange
            var cpfAttribute = new CPFAttribute();

            // Act
            var isValid = cpfAttribute.IsValid("1234567890A");

            // Assert
            Assert.False(isValid);
        }

    }
}
