using Microsoft.Extensions.DependencyInjection;
using MediatR;
using Ambev.DeveloperEvaluation.Application.Commands.Handlers;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM.Repositories;

namespace Ambev.DeveloperEvaluation.IoC
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services)
        {
            // Registrar MediatR e Handlers
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSaleHandler).Assembly));

            // Registrar repositórios
            services.AddScoped<ISaleRepository, SaleRepository>();

            // Outras dependências, caso necessário
            // services.AddScoped<IService, ServiceImplementation>();

            return services;
        }
    }
}
