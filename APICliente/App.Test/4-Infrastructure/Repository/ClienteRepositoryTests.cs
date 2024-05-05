using App.Test._4_Infrastructure.Context;
using Data.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Data.Tests.Repository
{
    public class ClienteRepositoryTests
    {
        private readonly MySQLContextTests _context;
        private readonly ClienteRepository _repository;

        public ClienteRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<MySQLContextTests>();
            optionsBuilder.UseInMemoryDatabase(databaseName: "TestDatabase");
            var options = optionsBuilder.Options;

            _context = new MySQLContextTests(options);
            _repository = new ClienteRepository(_context);
        }


        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "GetClientes_DeveRetornarTodosOsClientes")]
        public async Task GetClientes_DeveRetornarTodosOsClientes()
        {
            // Arrange
            var clientes = new List<Cliente>
            {
                new Cliente { IdCliente = 1, Nome = "Cliente 1", Sobrenome= "Teste1", CPF = "12345678901", Email = "teste1@email.com" },
                new Cliente { IdCliente = 2, Nome = "Cliente 2", Sobrenome= "Teste2", CPF = "98765432109", Email = "teste2@email.com" },
                new Cliente { IdCliente = 3, Nome = "Cliente 3", Sobrenome= "Teste3", CPF = "45678912345", Email = "teste3@email.com" }
            };

            _context.Cliente.AddRange(clientes);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetClientes();

            // Assert
            Assert.Equal(clientes, result);
        }


        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "GetClientes_DeveRetornarNuloQuandoNaoHaClientes")]
        public async Task GetClientes_DeveRetornarNuloQuandoNaoHaClientes()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetClientes();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "GetClienteById_DeveRetornarClientePorId")]
        public async Task GetClienteById_DeveRetornarClientePorId()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 5, Nome = "Cliente 10", Sobrenome = "Teste10", CPF = "12345678901", Email = "teste@email.com" };

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetClienteById(cliente.IdCliente);

            // Assert
            Assert.Equal(cliente, result);

        }


        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "GetClienteById_DeveRetornarNuloQuandoClienteNaoExiste")]
        public async Task GetClienteById_DeveRetornarNuloQuandoClienteNaoExiste()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 10, Nome = "Cliente 10", Sobrenome = "Teste10", CPF = "12345678901", Email = "teste@email.com.br" };

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetClienteById(20);

            // Assert
            Assert.Null(result);
        }

        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "GetClienteByCpf_DeveRetornarClientePorCpf")]
        public async Task GetClienteByCpf_DeveRetornarClientePorCpf()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 101, Nome = "Cliente 101", Sobrenome = "Teste101", CPF = "12345678901", Email = "teste@email.com" };

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetClienteByCpf(cliente.CPF);

            // Assert
            Assert.Equal(cliente, result);
        }

        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "GetClienteByCpf_DeveRetornarNuloQuandoClienteNaoExiste")]
        public async Task GetClienteByCpf_DeveRetornarNuloQuandoClienteNaoExiste()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 102, Nome = "Cliente 102", Sobrenome = "Teste101", CPF = "12345678901", Email = "teste@email.com.br" };

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetClienteByCpf("12345678902");

            // Assert
            Assert.Null(result);
        }

        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "PostCliente_DeveAdicionarCliente")]
        public async Task PostCliente_DeveAdicionarCliente()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 10, Nome = "Cliente 10", Sobrenome = "Teste10", CPF = "12345678901", Email = "teste@email.com.br" };

            // Act
            var result = await _repository.PostCliente(cliente);

            // Assert
            Assert.Equal(cliente, result);
        }

        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "UpdateCliente_DeveAtualizarCliente")]
        public async Task UpdateCliente_DeveAtualizarCliente()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 103, Nome = "Cliente 103", Sobrenome = "Teste103", CPF = "12345678901", Email = "teste@email.com.br" };

            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            cliente.Nome = "Cliente 113";
            var result = await _repository.UpdateCliente(cliente);

            // Assert
            Assert.Equal(1, result);
        }

        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "DeleteCliente_DeveExcluirCliente")]
        public async Task DeleteCliente_DeveExcluirCliente()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 10, Nome = "Cliente 10", Sobrenome = "Teste10", CPF = "12345678901", Email = "teste@email.com.br" };
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteCliente(cliente.IdCliente);

            // Assert
            Assert.Equal(1, result);
        }

        #region [Dispose]
        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "Dispose_DeveDescartarContexto")]
        public void Dispose_DeveDescartarContexto()
        {
            // Arrange
            _context.Cliente.RemoveRange(_context.Cliente);
            var cliente = new Cliente { IdCliente = 106, Nome = "Cliente 106", Sobrenome = "Teste107", CPF = "12345678901", Email = "teste1@email.com" };

            _context.Cliente.Add(cliente);
            _context.SaveChanges();

            // Act
            _repository.Dispose();

            // Assert
            Assert.NotNull(_context);
        }

        [Trait("Categoria", "ClienteRepository")]
        [Fact(DisplayName = "Dispose")]
        public void Dispose()
        {
            //act
            _repository.Dispose();
            Assert.NotNull(_context);
        }
        #endregion

    }
}