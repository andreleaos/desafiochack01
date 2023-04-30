using Desafio01WebApp.Models.Dtos;
using Newtonsoft.Json;

namespace Desafio01WebApp.Models.Infrastructure.Client
{
    public class ClientPesquisaHttp : IClientPesquisaHttp
    {
        public async Task<UrlDesafio> Get(string url, string parametro)
        {
            try
            {
                HttpClient client = new HttpClient();

                var urlBase = $"{url}/{parametro}";
                client.BaseAddress = new Uri(urlBase);
                HttpResponseMessage response = await client.GetAsync(urlBase);

                var content = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<UrlDesafio>(content);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
