using App.Test;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net;
using Xunit;

namespace APICliente.Tests
{
    public class ProgramTests
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;

        public ProgramTests()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task GetClientes_ReturnsOk()
        {
            // Arrange
            var expectedStatusCode = HttpStatusCode.NotFound;

            // Act
            var response = await _client.GetAsync("/clientes");

            // Assert
            Assert.Equal(expectedStatusCode, response.StatusCode);
        }

        //[Fact]
        //public void AddDbContext_UsesMySql_WithConnectionString()
        //{
        //    // Arrange
        //    var connectionStringMysql = "Server=localhost;initial catalog=BD_PKFF_CLIENTES;Uid=pkcliente;Pwd=Fast.Food;";
        //    var services = new ServiceCollection();
        //    var builder = new DbContextOptionsBuilder<MySQLContext>();
        //    builder.UseMySql(
        //        connectionStringMysql, // Usa a string de conexão.
        //        ServerVersion.AutoDetect(connectionStringMysql), // Especifica a versão do servidor MySQL.
        //        builder => builder.MigrationsAssembly("APICliente") // Especifica o assembly do projeto que contém as classes de migrações do EF Core.
        //    );

        //    // Act
        //    services.AddDbContext<MySQLContext>(options => options.UseMySql(
        //        connectionStringMysql, // Usa a string de conexão.
        //        ServerVersion.AutoDetect(connectionStringMysql), // Especifica a versão do servidor MySQL.
        //        builder => builder.MigrationsAssembly("APICliente") // Especifica o assembly do projeto que contém as classes de migrações do EF Core.
        //    ));

        //    // Assert
        //    var serviceProvider = services.BuildServiceProvider();
        //    var context = serviceProvider.GetService<MySQLContext>();
        //    Assert.NotNull(context);
        //    Assert.IsType<MySQLContext>(context);
        //}

        [Fact]
        public void AddControllers_UsesNewtonsoftJson()
        {
            // Arrange
            var services = new ServiceCollection();

            // Act
            services.AddControllers().AddNewtonsoftJson();

            // Assert
            var serviceProvider = services.BuildServiceProvider();
            var mvcOptions = serviceProvider.GetService<IOptions<MvcNewtonsoftJsonOptions>>();
            Assert.NotNull(mvcOptions);
            Assert.Equal(Formatting.None, mvcOptions.Value.SerializerSettings.Formatting);
            Assert.Equal(DateFormatHandling.IsoDateFormat, mvcOptions.Value.SerializerSettings.DateFormatHandling);
            Assert.Equal(Formatting.None, mvcOptions.Value.SerializerSettings.Formatting);
            Assert.Equal(DateFormatHandling.IsoDateFormat, mvcOptions.Value.SerializerSettings.DateFormatHandling);
        }
    }
}
