

using Microsoft.AspNetCore.Mvc.DataAnnotations;
using NewCentury.Data.Context;
using NewCentury.Data.Repository;
using NewCentury.Domain.Intefaces;
using NewCentury.Domain.Notificacoes;
using NewCentury.Service.Services;

namespace NewCentury.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<MeuDbContext>();
            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IJogadorService, JogadorService>();
            services.AddScoped<IJogadorRepository, JogadorRepository>();

            services.AddScoped<IHistoricoTentativaService, HistoricoTentativaService>();
            services.AddScoped<IHistoricoTentativaRepository, HistoricoTentativaRepository>();

            services.AddScoped<IPartidaService, PartidaService>();
            services.AddScoped<IPartidaRepository, PartidaRepository>();

            services.AddScoped<IRodadaService, RodadaService>();
            services.AddScoped<IRodadaRepository, RodadaRepository>();

            services.AddScoped<IGameService, GameService>();

            return services;
        }
    }
}