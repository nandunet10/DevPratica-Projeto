
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Infra.Entity
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }

        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Categorias> Categorias { get; set; }
        public DbSet<Veiculos> Veiculos { get; set; }
        public DbSet<FormasDePagamento> FormasDePagamento { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }
    }
}
