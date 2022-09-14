using CarLocadora.Modelo.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Negocio.Locacoes
{
    public interface ILocacoesNegocio
    {
        Task<List<LocacoesModel>> ObterLista();
        Task Inserir(LocacoesModel model);
        Task Alterar(LocacoesModel model);
        Task<LocacoesModel> Obter(int id);
    }
}
