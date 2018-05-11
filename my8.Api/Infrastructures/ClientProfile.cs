using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Infrastructures
{
    public static class ClientProfile
    {
        public static void Config(IServiceCollection services, IMapperConfigurationExpression mapper)
        {
            ConfigServices(services);
            Errors.Init();
        }
        private static void ConfigServices(IServiceCollection services)
        {
            services.AddScoped<CurrentProcess>();
        }
    }
}
