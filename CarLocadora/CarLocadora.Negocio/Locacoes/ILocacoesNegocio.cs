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
        List<LocacoesModel> ObterLista();
        void Inserir(LocacoesModel model);
        void Alterar(LocacoesModel model);
        LocacoesModel Obter(int id);
    }
}
