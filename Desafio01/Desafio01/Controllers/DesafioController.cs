using Desafio01.Models.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Validations;
using Newtonsoft.Json;
using Npgsql;
using System.Data;

namespace Desafio01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesafioController : ControllerBase
    {
        private readonly IConfiguration configuration;
        public DesafioController(IConfiguration configuration)
        {
            this.configuration= configuration;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            string connStr = configuration.GetConnectionString("desafiodb");
            var query = "select power_name from powers";

            try
            {
                NpgsqlConnection cn = new NpgsqlConnection(connStr);
                var cmd = new NpgsqlCommand(query, cn);
                DataSet ds = new DataSet();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);

                string param = string.Empty;

                foreach(DataRow item in ds.Tables[0].Rows)
                {
                    param = item.ItemArray[0].ToString();
                }

                HttpClient client = new HttpClient();

                var url = $"https://fiap-dotnet.azurewebsites.net/Fiap/{param}";
                client.BaseAddress = new Uri(url);
                HttpResponseMessage response = await client.GetAsync(url);

                var result = response.Content.ReadAsStringAsync().Result;
                var resultApi = JsonConvert.DeserializeObject<UrlDesafio1>(result);

                client = new HttpClient();
                var url2 =  $"https://fiap-dotnet.azurewebsites.net/Power/{resultApi.Message}";
                client.BaseAddress = new Uri(url2);
                HttpResponseMessage response2 = await client.GetAsync(url2);

                var result2 = response2.Content.ReadAsStringAsync().Result;
                var resultApi2 = JsonConvert.DeserializeObject<UrlDesafio2>(result2);

                return Ok(resultApi2.ImageUrl);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
