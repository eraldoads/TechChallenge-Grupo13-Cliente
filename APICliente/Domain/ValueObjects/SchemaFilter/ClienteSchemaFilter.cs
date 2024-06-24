using Domain.Entities;
using Domain.EntitiesDTO;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Domain.ValueObjects
{
    public class ClienteSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            // Verifica se o contexto é da classe Cliente.
            if (context.Type == typeof(Cliente))
            {
                // Cria um objeto OpenApiObject com os valores desejados.
                var modeloCliente = new OpenApiObject
                {
                    ["idCliente"] = new OpenApiInteger(0),
                    ["nome"] = new OpenApiString("string"),
                    ["sobrenome"] = new OpenApiString("string"),
                    ["cpf"] = new OpenApiString("string"),
                    ["email"] = new OpenApiString("string"),
                    ["endereco"] = new OpenApiString("string"),
                    ["numeroTelefone"] = new OpenApiString("string"),
                    ["ativo"] = new OpenApiBoolean(true),
                    ["dataInativacao"] = new OpenApiDateTime(DateTimeOffset.UtcNow)
                };
                // Atribui o exemplo ao esquema.
                schema.Example = modeloCliente;
            }

            // Verifica se o contexto é da classe ClienteDTO.
            if (context.Type == typeof(ClienteDTO))
            {
                // Cria um objeto OpenApiObject com os valores desejados.
                var modeloCliente = new OpenApiObject
                {
                    ["nome"] = new OpenApiString("string"),
                    ["sobrenome"] = new OpenApiString("string"),
                    ["cpf"] = new OpenApiString("string"),
                    ["email"] = new OpenApiString("string"),
                    ["endereco"] = new OpenApiString("string"),
                    ["numeroTelefone"] = new OpenApiString("string")
                };
                // Atribui o exemplo ao esquema.
                schema.Example = modeloCliente;
            }
        }
    }
}
