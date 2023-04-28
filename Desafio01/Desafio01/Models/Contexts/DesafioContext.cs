using Microsoft.EntityFrameworkCore;

namespace Desafio01.Models.Contexts
{
    public class DesafioContext : DbContext
    {
        public DesafioContext(DbContextOptions opt)
            :base(opt)
        {

        }

        //public DbSet<>
    }
}
