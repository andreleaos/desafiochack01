using Npgsql;
using System.Data;

namespace Desafio01WebApp.Models.Infrastructure.Repositories
{
    public class DesafioRepository : IDesafioRepository, IDisposable
    {
        private static NpgsqlConnection cn = null;

        public string Execute(string query, string connectionString)
        {
            string result = null;
            NpgsqlCommand cmd = null;
            DataSet ds = null;
            NpgsqlDataAdapter da = null;

            try
            {
                if (cn == null)
                    cn = new NpgsqlConnection(connectionString);

                cmd = new NpgsqlCommand(query, cn);
                ds = new DataSet();
                da = new NpgsqlDataAdapter(cmd);
                da.Fill(ds);

                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    result = item.ItemArray[0].ToString();
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cmd = null;
                ds = null;
                da = null;
            }
        }

        public void Dispose()
        {
            cn = null;
        }

    }
}
