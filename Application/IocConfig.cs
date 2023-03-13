using Application.Classes;
using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class IocConfig
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IBeerManager, BeerManager>();
            services.AddScoped<IBrewerManager, BrewerManager>();
            services.AddScoped<IVendorManager, VendorManager>();
            services.AddScoped<IVendorBeerManager, VendorBeerManager>();

            return services;
        }
    }
}
