using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.ManutencaoVeiculo
{
    public class ManutencaoVeiculoNegocio : IManutencaoVeiculoNegocio
    {
        private readonly Context _context;
        public ManutencaoVeiculoNegocio(Context context)
        {
            _context = context;
        }

        public async Task Alterar(ManutencaoVeiculoModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            ManutencaoVeiculoModel model = _context.ManutencaoVeiculos.SingleOrDefault(x => x.Id.Equals(id));
            _context.Remove(model);
            await _context.SaveChangesAsync();
        }

        public async Task Inserir(ManutencaoVeiculoModel model)
        {
            model.DataInclusao = DateTime.Now;
            await _context.AddAsync(model);
            await _context.SaveChangesAsync();
        }
        public async Task<ManutencaoVeiculoModel> Obter(int id) => await _context.ManutencaoVeiculos.SingleOrDefaultAsync(x => x.Id.Equals(id));

        public async Task<List<ManutencaoVeiculoModel>> ObterLista() => await _context.ManutencaoVeiculos.ToListAsync();

    }
}
