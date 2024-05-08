using API.Controllers;
using Application.Interfaces;
using Domain.Entities;
using Domain.EntitiesDTO;
using Domain.ValueObjects;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Domain.Tests._1_WebAPI
{
    public class ClienteControllerTests
    {
        private readonly ClienteController _controller;
        private readonly Mock<IClienteService> _AppService = new();

        public ClienteControllerTests()
        {
            _controller = new ClienteController(_AppService.Object);
        }

        #region [GET]
        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "BuscarListaClientes OkResult")]
        public async Task GetClientes_ReturnsOkResult_BuscarListaClientes()
        {
            // Arrange
            _AppService.Setup(service => service.GetClientes())
                .ReturnsAsync([new(), new()]);

            // Act
            var result = await _controller.GetClientes();

            // Assert
            Assert.NotNull(result);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "BuscarIdCliente OkResult")]
        public async Task GetCliente_ReturnsOkResult_BuscarIdCliente()
        {
            // Arrange
            Cliente cliente = new()
            {
                IdCliente = 10,
                CPF = "123.456.789.01",
                Email = "testCliente@email.com",
                Nome = "Testenome",
                Sobrenome = "TesteSobrenome",
            };

            _AppService.Setup(service => service.GetClienteById(cliente.IdCliente))
                    .ReturnsAsync(cliente);

            // Act
            var result = await _controller.GetCliente(cliente.IdCliente);

            // Assert
            var actionResult = Assert.IsType<ActionResult<Cliente?>>(result);
            Assert.NotNull(actionResult.Value);
            Assert.Equal(cliente, actionResult.Value);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "BuscarIdCliente NoContentResult")]
        public async Task GetCliente_ReturnsNoContentResult_BuscarIdCliente()
        {
            // Arrange
            int testId = 1;
            Cliente? cliente = null;

            _AppService.Setup(service => service.GetClienteById(testId))
                .ReturnsAsync(cliente);

            // Act
            var result = await _controller.GetCliente(testId);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }
        #endregion

        #region [POST]
        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PostCliente OkResult")]
        public async Task PostCliente_ReturnsOkResult()
        {
            // Arrange
            var clienteDTO = new ClienteDTO { CPF = "123.456.789-10" };
            var cliente = new Cliente { CPF = "311.234.567-89" };
            _AppService.Setup(service => service.PostCliente(clienteDTO))
                .ReturnsAsync(cliente);

            // Act
            var result = await _controller.PostCliente(clienteDTO);

            // Assert
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PostCliente BadRequestResult ValidationException")]
        public async Task PostCliente_ReturnsBadRequestResult_ValidationException()
        {
            // Arrange
            var clienteDTO = new ClienteDTO
            {
                Nome = "Testenome",
                Sobrenome = "TesteSobrenome",
                CPF = "123.456.789.01",
                Email = "testCliente@email.com"
            };

            _AppService.Setup(service => service.PostCliente(clienteDTO))
                .Throws(new ValidationException("Erro de validação"));

            // Act
            var result = await _controller.PostCliente(clienteDTO);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Erro de validação", badRequestResult.Value);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PostCliente StatusCode500Result Exception")]
        public async Task PostCliente_ReturnsStatusCode500Result_Exception()
        {
            // Arrange
            var clienteDTO = new ClienteDTO
            {
                Nome = "Testenome",
                Sobrenome = "TesteSobrenome",
                CPF = "123.456.789.01",
                Email = "testCliente@email.com"
            };

            _AppService.Setup(service => service.PostCliente(clienteDTO))
                .Throws(new Exception());

            // Act
            var result = await _controller.PostCliente(clienteDTO);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        #endregion

        #region [PATCH]
        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PatchCliente NoContentResult")]
        public async Task PatchCliente_ReturnsNoContentResult()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var patchDoc = new JsonPatchDocument<Cliente>(); // preencha com operações de patch de teste
            _AppService.Setup(service => service.PatchCliente(id, patchDoc))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PatchCliente(id, patchDoc);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PatchCliente NotFoundResult ResourceNotFoundException")]
        public async Task PatchCliente_ReturnsNotFoundResult_ResourceNotFoundException()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var patchDoc = new JsonPatchDocument<Cliente>(); // preencha com operações de patch de teste
            _AppService.Setup(service => service.PatchCliente(id, patchDoc))
                .Throws(new ResourceNotFoundException("Cliente não encontrado"));

            // Act
            var result = await _controller.PatchCliente(id, patchDoc);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Cliente não encontrado", notFoundResult.Value);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PatchCliente BadRequestResult ValidationException")]
        public async Task PatchCliente_ReturnsBadRequestResult_ValidationException()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var patchDoc = new JsonPatchDocument<Cliente>(); // preencha com operações de patch de teste
            _AppService.Setup(service => service.PatchCliente(id, patchDoc))
                .Throws(new ValidationException("Erro de validação"));

            // Act
            var result = await _controller.PatchCliente(id, patchDoc);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro de validação", badRequestResult.Value);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PatchCliente StatusCode500Result Exception")]
        public async Task PatchCliente_ReturnsStatusCode500Result_Exception()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var patchDoc = new JsonPatchDocument<Cliente>(); // preencha com operações de patch de teste
            _AppService.Setup(service => service.PatchCliente(id, patchDoc))
                .Throws(new Exception());

            // Act
            var result = await _controller.PatchCliente(id, patchDoc);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }
        #endregion

        #region [PUT]
        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PutCliente NoContentResult")]
        public async Task PutCliente_ReturnsNoContentResult()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var clienteInput = new Cliente { IdCliente = 1 };
            _AppService.Setup(service => service.PutCliente(id, clienteInput))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.PutCliente(id, clienteInput);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PutCliente NotFoundResult ResourceNotFoundException")]
        public async Task PutCliente_ReturnsNotFoundResult_ResourceNotFoundException()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var clienteInput = new Cliente { /* preencha com dados de teste */ };
            _AppService.Setup(service => service.PutCliente(id, clienteInput))
                .Throws(new ResourceNotFoundException("Cliente não encontrado"));

            // Act
            var result = await _controller.PutCliente(id, clienteInput);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Cliente não encontrado", notFoundResult.Value);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PutCliente BadRequestResult ValidationException")]
        public async Task PutCliente_ReturnsBadRequestResult_ValidationException()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var clienteInput = new Cliente { /* preencha com dados de teste */ };
            _AppService.Setup(service => service.PutCliente(id, clienteInput))
                .Throws(new ValidationException("Erro de validação"));

            // Act
            var result = await _controller.PutCliente(id, clienteInput);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal("Erro de validação", badRequestResult.Value);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "PutCliente StatusCode500Result Exception")]
        public async Task PutCliente_ReturnsStatusCode500Result_Exception()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var clienteInput = new Cliente { /* preencha com dados de teste */ };
            _AppService.Setup(service => service.PutCliente(id, clienteInput))
                .Throws(new Exception());

            // Act
            var result = await _controller.PutCliente(id, clienteInput);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, objectResult.StatusCode);
        }

        #endregion

        #region [DELETE]
        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "DeleteCliente OkResult")]
        public async Task DeleteCliente_ReturnsOkResult()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            var cliente = new Cliente { IdCliente = 1 };
            _AppService.Setup(service => service.DeleteCliente(id))
                .ReturnsAsync(cliente);

            // Act
            var result = await _controller.DeleteCliente(id);

            // Assert
            Assert.IsType<ActionResult<Cliente>>(result);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "DeleteCliente NoContentResult ClienteIsNull")]
        public async Task DeleteCliente_ReturnsNoContentResult_ClienteIsNull()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            _AppService.Setup(service => service.DeleteCliente(id))
                .ReturnsAsync((Cliente)null);

            // Act
            var result = await _controller.DeleteCliente(id);

            // Assert
            Assert.IsType<NoContentResult>(result.Result);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "DeleteCliente NotFoundResult KeyNotFoundException")]
        public async Task DeleteCliente_ReturnsNotFoundResult_KeyNotFoundException()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            _AppService.Setup(service => service.DeleteCliente(id))
                .Throws(new KeyNotFoundException());

            // Act
            var result = await _controller.DeleteCliente(id);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result.Result);
            var value = Assert.IsAssignableFrom<object>(notFoundResult.Value);
            Assert.Equal(new { id, error = "Cliente não encontrado" }.ToString(), value.ToString());
        }


        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "DeleteCliente BadRequestResult ValidationException")]
        public async Task DeleteCliente_ReturnsBadRequestResult_ValidationException()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            _AppService.Setup(service => service.DeleteCliente(id))
                .Throws(new ValidationException("Erro de validação"));

            // Act
            var result = await _controller.DeleteCliente(id);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result.Result);
            Assert.Equal("Erro de validação", badRequestResult.Value);
        }

        [Trait("Categoria", "ClienteController")]
        [Fact(DisplayName = "DeleteCliente StatusCode500Result Exception")]
        public async Task DeleteCliente_ReturnsStatusCode500Result_Exception()
        {
            // Arrange
            int id = 1; // substitua pelo ID de teste
            _AppService.Setup(service => service.DeleteCliente(id))
                .Throws(new Exception());

            // Act
            var result = await _controller.DeleteCliente(id);

            // Assert
            var objectResult = Assert.IsType<ObjectResult>(result.Result);
            Assert.Equal(500, objectResult.StatusCode);
        }
        #endregion
    }
}
