namespace Desafio01WebApp.Models.Infrastructure.Repositories
{
    public interface IDesafioRepository
    {

        string Execute(string query, string connectionString);
    }
}
