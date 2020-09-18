using Dapper.Application.Interfaces;
using Dapper.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dapper.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection service)
        {
            service.AddTransient<IProductRepository, ProductRepository>();
            service.AddTransient<IUnitOfWork, UnitOfWOrk>();
        }
    }
}
