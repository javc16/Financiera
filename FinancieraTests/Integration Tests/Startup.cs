using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraTests.Integration_Tests
{
    using Financiera.DBContext;
    using Financiera.DBContext.Repository;
    using Financiera.Domain;
    using Financiera.Models;
    using Financiera.Services;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    namespace YourTestProject
    {
        public class TestStartup
        {
            public void ConfigureServices(IServiceCollection services)
            {
                // Configure Entity Framework to use an in-memory database
                services.AddDbContext<FinancieraContext>(options =>
                {
                    options.UseInMemoryDatabase("DBContext");
                });
                services.AddScoped<ClienteService>();
                services.AddScoped<ClienteDomain>();

                services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
                services.AddScoped<IRepository<Genero>, Repository<Genero>>();

                // Register your services and repositories here
                // Example: services.AddScoped<IRepository<Cliente>, Repository<Cliente>>();
                // Add any other dependencies needed for your tests
            }
        }
    }

}
