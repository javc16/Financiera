using AutoMapper;
using Financiera.Constants;
using Financiera.DBContext.Repository;
using Financiera.Domain;
using Financiera.Models.DTOs;
using Financiera.Models;
using Financiera.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Linq.Expressions;

namespace FinancieraTests.Unit_Tests
{
    public class ClienteServicesTests
    {
        [Test]
        public async Task GetById_ExistingClient_ReturnsSuccessResponse()
        {
            // Arrange
            var clientId = 1;
            var genderId = 1;
            var etc = DateTime.Now.AddYears(-20);

            var expectedGender = new Genero
            {
                Id = 1,
                Name = "Femenino"
            };

            var expectedCliente = new Cliente 
            { 
                Id = clientId,
                Nombre = "Maria Wong",
                Contrasena = "test",
                Direccion = "test",
                FechaNacimiento =DateTime.Now.AddYears(-20),
                Genero = expectedGender,
                Identificacion = "0501-2000-11111",
                Telefono = "5555-5555",
                Estado = true
            };

         

            var clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            clienteRepositoryMock.Setup(repo => repo.GetById(clientId, It.IsAny<Expression<Func<Cliente, object>>>()))
                .ReturnsAsync(expectedCliente);

            var genderRepositoryMock = new Mock<IRepository<Genero>>();
            genderRepositoryMock.Setup(repo => repo.GetById(genderId, It.IsAny<Expression<Func<Genero, object>>>()))
                .ReturnsAsync(expectedGender);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<ClienteDTO>(expectedCliente))
                .Returns(new ClienteDTO { Id = clientId, Nombre = "Maria Wong" });

            var clienteService = new ClienteService(
                clienteRepositoryMock.Object,
                genderRepositoryMock.Object,
                mapperMock.Object,
                new ClienteDomain() 
            );

            // Act
            var response = await clienteService.GetById(clientId);

            // Assert
            Assert.That(response.Status, Is.EqualTo(Constantes.Sucess));
            Assert.That(response.Message, Is.EqualTo("Retrieved successfully"));
            Assert.NotNull(response.Data);
        }

        [Test]
        public void GetAll_ReturnsThreeItems()
        {
            // Arrange
            var genderId = 1;
            var expectedGender = new Genero
            {
                Id = 1,
                Name = "Femenino"
            };           

            var expectedClientes = new List<Cliente>
            {
                new Cliente
                {
                    Id = 1,
                    Nombre = "Maria Wong",
                    Contrasena = "test",
                    Direccion = "test",
                    FechaNacimiento = DateTime.Now.AddYears(-20),
                    Genero = expectedGender,
                    Identificacion = "0501-2000-11112",
                    Telefono = "5555-5555",
                    Estado = true
                },new Cliente
                {
                    Id = 2,
                    Nombre = "Maria Wong",
                    Contrasena = "test",
                    Direccion = "test",
                    FechaNacimiento = DateTime.Now.AddYears(-20),
                    Genero = expectedGender,
                    Identificacion = "0501-2000-11113",
                    Telefono = "5555-5555",
                    Estado = true
                },new Cliente
                {
                    Id = 3,
                    Nombre = "Maria Wong",
                    Contrasena = "test",
                    Direccion = "test",
                    FechaNacimiento = DateTime.Now.AddYears(-20),
                    Genero = expectedGender,
                    Identificacion = "0501-2000-11111",
                    Telefono = "5555-5555",
                    Estado = true
                },
        };


            var clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            clienteRepositoryMock.Setup(repo => repo.Find(
                It.IsAny<Expression<Func<Cliente, bool>>>(),
                It.IsAny<Expression<Func<Cliente, object>>[]>()))
                .Returns(expectedClientes);

            var genderRepositoryMock = new Mock<IRepository<Genero>>();
            genderRepositoryMock.Setup(repo => repo.GetById(genderId, It.IsAny<Expression<Func<Genero, object>>>()))
                .ReturnsAsync(expectedGender);

            var mapperMock = new Mock<IMapper>();

            var clienteService = new ClienteService(
                clienteRepositoryMock.Object,
                genderRepositoryMock.Object,
                mapperMock.Object,
                new ClienteDomain() // You can mock this if needed
            );

            // Act
            var clientes = clienteService.GetAll();

            // Assert
            Assert.That(clientes.Count(), Is.EqualTo(3)); // Check if the list contains three items
        }

        [Test]
        public async Task GetById_NonExistingClient_ReturnsNotFoundResponse()
        {
            // Arrange
            var clientId = 2;
            var genderId = 2;

            var clienteRepositoryMock = new Mock<IRepository<Cliente>>();
            clienteRepositoryMock.Setup(repo => repo.GetById(clientId, It.IsAny<Expression<Func<Cliente, object>>>()))
                                 .ReturnsAsync((Cliente)null);

            
            var genderRepositoryMock = new Mock<IRepository<Genero>>();
            genderRepositoryMock.Setup(repo => repo.GetById(genderId, It.IsAny<Expression<Func<Genero, object>>>()))
                .ReturnsAsync((Genero)null);

            var mapperMock = new Mock<IMapper>();

            var clienteService = new ClienteService(
                clienteRepositoryMock.Object,
                genderRepositoryMock.Object,
                mapperMock.Object,
                new ClienteDomain() // You can mock this if needed
            );

            // Act
            var response = await clienteService.GetById(clientId);

            // Assert
            Assert.That(response.Status, Is.EqualTo(Constantes.Failed));
            Assert.IsTrue(response.Message?.Contains(clientId.ToString()));
            Assert.IsNull(response.Data);
        }

    


    }
}
