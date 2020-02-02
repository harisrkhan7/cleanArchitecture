using System;
using cleanArchitecture.Core.Entities.ProductAggregate;
using cleanArchitecture.Core.Interfaces.Repositories;
using cleanArchitecture.Infra.Data;
using cleanArchitecture.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace cleanArchitecture.Infra
{
    public static class InfrastructureDependencyInjection
    {
        public static void ConfigureInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IAsyncRepository<Product>, EfRepository<Product>>();
            
            
        }

    }
}
