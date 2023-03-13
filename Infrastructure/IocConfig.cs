using Application.Classes;
using Application.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class IocConfig
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IContext, Infrastructure.Context.Context>();
            services.AddDbContext<Infrastructure.Context.Context>(options =>
            {
                options.UseInMemoryDatabase("BeerVendorDb");
            });

            return services;
        }
    }
}
