using Desafio01WebApp.Models.Dtos;

namespace Desafio01WebApp.Models.Infrastructure.Client
{
    public interface IClientPesquisaHttp
    {
        Task<UrlDesafio> Get(string url, string parametro);
    }
}
