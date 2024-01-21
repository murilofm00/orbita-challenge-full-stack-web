using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Orbita.BackEnd.Api.Controllers;
using Orbita.BackEnd.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orbita.BackEnd.Tests
{
    public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        private readonly IServiceScope _scope;
        protected readonly OrbitaContext _context;

        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            _scope = factory.Services.CreateScope();

            _context = _scope.ServiceProvider.GetRequiredService<OrbitaContext>();
        }
    }
}
