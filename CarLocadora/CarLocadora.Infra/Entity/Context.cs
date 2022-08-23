
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;

namespace CarLocadora.Infra.Entity
{
    public class Context: DbContext
    {
        public Context(DbContextOptions<Context> options): base(options) { }

        public DbSet<ClienteModel> Clientes { get; set; }
        public DbSet<CategoriaModel> Categorias { get; set; }
        public DbSet<VeiculoModel> Veiculos { get; set; }
        public DbSet<FormasDePagamentoModel> FormasDePagamento { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
        public DbSet<ManutencaoVeiculoModel> ManutencaoVeiculos { get; set; }
    }
}
