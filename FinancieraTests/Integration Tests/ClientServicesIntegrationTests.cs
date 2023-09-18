using Financiera.DBContext.Repository;
using Financiera.DBContext;
using Financiera.Models;
using Financiera.Services;
using FinancieraTests.Integration_Tests.YourTestProject;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using AutoMapper;
using Moq;
using System.Linq.Expressions;
using Financiera.Domain;
using Financiera.Models.DTOs;

namespace FinancieraTests.Integration_Tests
{
    public class ClientServicesIntegrationTests
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ClientServicesIntegrationTests()
        {
            var services = new ServiceCollection();
            var startup = new TestStartup();
            startup.ConfigureServices(services);
            var serviceProvider = services.BuildServiceProvider();
            _scopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
        }

        [Fact]
        public void TestGetAll_ReturnsThreeItems()
        {
            // Arrange
            using (var scope = _scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<FinancieraContext>();
                var clienteRepository = new Repository<Cliente>(dbContext);
                var genderReporistory = new Repository<Genero>(dbContext);

                var mapperConfig = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<Cliente, ClienteDTO>();
                    cfg.CreateMap<ClienteDTO, Cliente>();
                });

                var mapper = mapperConfig.CreateMapper();
                var mapperMock = new Mock<IMapper>();
                mapperMock.Setup(m => m.Map<IEnumerable<ClienteDTO>>(It.IsAny<IEnumerable<Cliente>>()))
                    .Returns((IEnumerable<Cliente> source) => mapper.Map<IEnumerable<ClienteDTO>>(source));

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

                var clienteService = new ClienteService(clienteRepository, genderReporistory, mapperMock.Object, new ClienteDomain());

                dbContext.AddRange(expectedClientes);
                dbContext.SaveChanges();

                var clientes = clienteService.GetAll();

                Assert.Equals(3, clientes.Count()); 
            }
        }

    }
}
