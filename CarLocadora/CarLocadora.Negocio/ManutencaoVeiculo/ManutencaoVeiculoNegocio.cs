using CarLocadora.Infra.Entity;
using CarLocadora.Modelo.Modelos;
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

        public void Alterar(ManutencaoVeiculoModel model)
        {
            model.DataAlteracao = DateTime.Now;
            _context.Update(model);
            _context.SaveChangesAsync();
        }

        public void Excluir(int id)
        {
            ManutencaoVeiculoModel model = _context.ManutencaoVeiculos.SingleOrDefault(x => x.Id.Equals(id));
            _context.Remove(model);
            _context.SaveChangesAsync();
        }

        public void Inserir(ManutencaoVeiculoModel model)
        {
            model.DataInclusao = DateTime.Now;
            _context.AddAsync(model);
            _context.SaveChangesAsync();
        }
        public ManutencaoVeiculoModel Obter(int id) => _context.ManutencaoVeiculos.SingleOrDefault(x => x.Id.Equals(id));

        public List<ManutencaoVeiculoModel> ObterLista() => _context.ManutencaoVeiculos.ToList();
  
    }
}
