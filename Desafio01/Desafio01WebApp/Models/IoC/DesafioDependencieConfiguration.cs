using Desafio01WebApp.Models.Infrastructure.Client;
using Desafio01WebApp.Models.Infrastructure.Repositories;

namespace Desafio01WebApp.Models.IoC
{
    public class DesafioDependencieConfiguration
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IDesafioRepository, DesafioRepository>();
            services.AddSingleton<IClientPesquisaHttp, ClientPesquisaHttp>();
        }
    }
}
