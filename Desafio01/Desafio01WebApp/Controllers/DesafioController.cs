using Desafio01WebApp.Models.Dtos;
using Desafio01WebApp.Models.Infrastructure.Client;
using Desafio01WebApp.Models.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Npgsql;
using System.Data;

namespace Desafio01WebApp.Controllers
{
    public class DesafioController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IDesafioRepository _desafioRepository;
        private readonly IClientPesquisaHttp _clientPesquisaHttp;

        private static string URL_BASE_01 = $"https://fiap-dotnet.azurewebsites.net/Fiap";
        private static string URL_BASE_02 = $"https://fiap-dotnet.azurewebsites.net/Power";

        public DesafioController(
            IConfiguration configuration,
            IDesafioRepository desafioRepository,
            IClientPesquisaHttp clientPesquisaHttp)
        {
            this.configuration = configuration;
            _desafioRepository = desafioRepository;
            _clientPesquisaHttp = clientPesquisaHttp;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            string connStr = configuration.GetConnectionString("desafiodb");
            var query = configuration["QuerySql:Query_01"];

            try
            {
                var param = _desafioRepository.Execute(query, connStr);

                UrlDesafio response01 = await _clientPesquisaHttp.Get(URL_BASE_01, param);
                UrlDesafio response02 = await _clientPesquisaHttp.Get(URL_BASE_02, response01.Message);
                return View(response02);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
