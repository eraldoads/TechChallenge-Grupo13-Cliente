using Application.Interfaces;
using Domain.Entities;
using Domain.EntitiesDTO;
using Domain.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace App.Test._2_Application.Services
{
    public class ClienteServiceTests
    {
        private readonly IClienteService _AppServices;
        private readonly Mock<IClienteRepository> _Repository = new();

        public ClienteServiceTests()
        {
            _AppServices = new ClienteService(_Repository.Object);

        }

        [Fact(DisplayName = "GetClienteById Retorna Cliente OK")]
        public async Task GetClienteById_ReturnsCorrectCliente()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var cliente = new Cliente
            {
                IdCliente = id,
                Nome = "João",
                Sobrenome = "Silva",
                CPF = "12345678900",
                Email = "teste@email.com.br"
            };

            _Repository.Setup(repo => repo.GetClienteById(id))
                .ReturnsAsync(cliente);

            // Act
            var result = await _AppServices.GetClienteById(id);

            // Assert
            Assert.Equal(cliente, result);
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "GetClienteByCpf Retorna Cliente")]
        public async Task GetClienteByCpf_ReturnsCorrectCliente()
        {
            // Arrange
            string cpf = "123.456.789-01"; // substitua pelo CPF de teste
            var cliente = new Cliente
            {
                IdCliente = 1,
                Nome = "João",
                Sobrenome = "Silva",
                CPF = cpf,
                Email = ""
            };

            _Repository.Setup(repo => repo.GetClienteByCpf(cpf))
                .ReturnsAsync(cliente);

            // Act
            var result = await _AppServices.GetClienteByCpf(cpf);

            // Assert
            Assert.Equal(cliente, result);
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "GetClientes Retorna Lista de Clientes")]
        public async Task GetClientes_ReturnsListOfClientes()
        {
            // Arrange
            var clientes = new List<Cliente> { new(), new() }; // substitua com dados de teste

            _Repository.Setup(repo => repo.GetClientes())
                .ReturnsAsync(clientes);

            // Act
            var result = await _AppServices.GetClientes();

            // Assert
            Assert.Equal(clientes, result);
        }


        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "PostCliente Returna Cliente OK")]
        public async Task PostCliente_ValidClienteDTO_ReturnsCliente()
        {
            // Arrange
            var clienteDTO = new ClienteDTO
            {
                Nome = "Joao",
                Sobrenome = "Silva",
                CPF = "48804616016",
                Email = "email@test.com.br"
            };

            _Repository.Setup(repo => repo.PostCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(new Cliente
                {
                    IdCliente = 1,
                    Nome = clienteDTO.Nome,
                    Sobrenome = clienteDTO.Sobrenome,
                    CPF = clienteDTO.CPF,
                    Email = clienteDTO.Email
                });

            // Act
            var result = await _AppServices.PostCliente(clienteDTO);

            // Assert
            Assert.Equal(clienteDTO.Nome, result.Nome);
            Assert.Equal(clienteDTO.Sobrenome, result.Sobrenome);
            Assert.Equal(clienteDTO.CPF, result.CPF);
            Assert.Equal(clienteDTO.Email, result.Email);
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "PostCliente Throws Valida Exception")]
        public async Task PostCliente_InvalidClienteDTO_ThrowsValidationException()
        {
            // Arrange
            var clienteDTO = new ClienteDTO { };

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _AppServices.PostCliente(clienteDTO));
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "PostCliente Throws ValidationException para CPD Exitente")]
        public async Task PostCliente_ExistingCPF_ThrowsValidationException()
        {
            // Arrange
            var clienteDTO = new ClienteDTO
            {
                Nome = "Joao",
                Sobrenome = "Silva",
                CPF = "48804616016",
                Email = "email@test.com.br"
            };

            var cliente = new Cliente
            {
                IdCliente = 1,
                Nome = clienteDTO.Nome,
                Sobrenome = clienteDTO.Sobrenome,
                CPF = clienteDTO.CPF,
                Email = clienteDTO.Email
            };
            _Repository.Setup(repo => repo.GetClienteByCpf(clienteDTO.CPF))
                .ReturnsAsync(cliente);

            // Act & Assert
            await Assert.ThrowsAsync<ValidationException>(() => _AppServices.PostCliente(clienteDTO));
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "PutCliente Atualiza Cliente Existente")]
        public async Task PutCliente_ExistingCliente_UpdatesCliente()
        {
            // Arrange
            int idCliente = 1; // substitua pelo ID de teste
            var clienteInput = new Cliente
            {
                IdCliente = idCliente,
                Nome = "Joao",
                Sobrenome = "Silva",
                CPF = "48804616016",
                Email = "teste@email.com"
            };
            var cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = "Joao",
                Sobrenome = "Silva",
                CPF = "48804616016",
                Email = "teste@email.com"
            };

            _Repository.Setup(repo => repo.GetClienteById(idCliente))
                .ReturnsAsync(cliente);
            _Repository.Setup(repo => repo.UpdateCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(1);

            // Act
            await _AppServices.PutCliente(idCliente, clienteInput);

            // Assert
            _Repository.Verify(repo => repo.UpdateCliente(It.IsAny<Cliente>()), Times.Once());
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "PatchCliente Atualiza Parcialmente Cliente Existente")]
        public async Task PatchCliente_ExistingCliente_PartiallyUpdatesCliente()
        {
            // Arrange
            int idCliente = 1; // substitua pelo ID de teste
            var patchDoc = new JsonPatchDocument<Cliente>(); // preencha com operações de patch de teste
            var cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = "Joao",
                Sobrenome = "Silva",
                CPF = "48804616016",
                Email = "teste@email.com"
            };

            _Repository.Setup(repo => repo.GetClienteById(idCliente))
                .ReturnsAsync(cliente);
            _Repository.Setup(repo => repo.UpdateCliente(It.IsAny<Cliente>()))
                .ReturnsAsync(1);

            // Act
            await _AppServices.PatchCliente(idCliente, patchDoc);

            // Assert
            _Repository.Verify(repo => repo.UpdateCliente(It.IsAny<Cliente>()), Times.Once());
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "DeleteCliente Exclui Cliente Existente")]
        public async Task DeleteCliente_ExistingCliente_DeletesCliente()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var cliente = new Cliente
            {
                IdCliente = id,
                Nome = "Joao",
                Sobrenome = "Silva",
                CPF = "48804616016",
                Email = "teste@email.com"
            };

            _Repository.Setup(repo => repo.GetClienteById(id))
                .ReturnsAsync(cliente);

            _Repository.Setup(repo => repo.DeleteCliente(id))
                .ReturnsAsync(1);

            // Act
            var result = await _AppServices.DeleteCliente(id);

            // Assert
            Assert.Equal(cliente, result);
            _Repository.Verify(repo => repo.DeleteCliente(id), Times.Once());
        }

        [Trait("Categoria", "ClienteService")]
        [Fact(DisplayName = "DeleteCliente Throws KeyNotFoundException para Cliente Inexistente")]
        public async Task DeleteCliente_NonExistingCliente_ThrowsKeyNotFoundException()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            _Repository.Setup(repo => repo.GetClienteById(id))
                .ReturnsAsync((Cliente?)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _AppServices.DeleteCliente(id));
        }

    }
}