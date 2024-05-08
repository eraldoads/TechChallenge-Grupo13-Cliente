using Domain.Entities;
using Domain.EntitiesDTO;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Xunit;

namespace Domain.ValueObjects.Tests
{
    public class ClienteSchemaFilterTests
    {
        private readonly ClienteSchemaFilter _clienteSchemaFilter;

        public ClienteSchemaFilterTests()
        {
            _clienteSchemaFilter = new ClienteSchemaFilter();
        }

        [Trait("Categoria", "SchemaFilter")]
        [Fact(DisplayName = "Aplicar Define Exemplo para Schema de Cliente")]
        public void Aplicar_DefineExemploParaSchemaCliente()
        {
            // Arrange
            var schema = new OpenApiSchema();
            var context = new SchemaFilterContext(typeof(Cliente), null, null);

            // Act
            _clienteSchemaFilter.Apply(schema, context);

            // Assert
            Assert.NotNull(schema.Example);
        }

        [Trait("Categoria", "SchemaFilter")]
        [Fact(DisplayName = "Aplicar Define Exemplo para Schema de ClienteDTO")]
        public void Aplicar_DefineExemploParaSchemaClienteDTO()
        {
            // Arrange
            var schema = new OpenApiSchema();
            var context = new SchemaFilterContext(typeof(ClienteDTO), null, null);

            // Act
            _clienteSchemaFilter.Apply(schema, context);

            // Assert
            Assert.NotNull(schema.Example);
        }

    }
}
