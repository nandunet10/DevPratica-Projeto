
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Infra.Entity
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }


    }
}
