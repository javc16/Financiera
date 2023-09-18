using AutoMapper;
using Financiera.Constants;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Models;
using Financiera.Models.DTOs;
using Financiera.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FinancieraTest.UnitTests
{
    public class ClientServicesTests
    {
        [Fact]
        public async Task GetById_ExistingClient_ReturnsSuccessResponse()
        {
            // Arrange
            var clientId = 1;
            var genderId = 1;
            var expectedCliente = new Cliente { Id = clientId, Nombre = "John Doe" };

            var clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            clienteRepositoryMock.Setup(repo => repo.GetById(clientId, It.IsAny<Expression<Func<Cliente, object>>>()))
                .ReturnsAsync(expectedCliente);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ClienteDTO>(expectedCliente))
                .Returns(new ClienteDTO { Id = clientId, Nombre = "John Doe" });
            var genderRepositoryMock = new Mock<IRepository<Genero>>();
            clienteRepositoryMock.Setup(repo => repo.GetById(genderId, It.IsAny<Expression<Func<Cliente, object>>>()))
                .ReturnsAsync(expectedCliente);

            var clienteService = new ClienteService(
                clienteRepositoryMock.Object,
                genderRepositoryMock.Object,
                // Mock other dependencies
                mapperMock.Object,
                new ClienteDomain() // You can mock this if needed
            );

            // Act
            var response = await clienteService.GetById(clientId);

            // Assert
            Assert.Equals(Constantes.Sucess, response.Status);
            Assert.Equals("Retrieved successfully", response.Message);
            Assert.IsNotNull(response.Data);
            
        }

    }
}
