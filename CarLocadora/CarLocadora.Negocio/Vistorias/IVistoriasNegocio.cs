using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.Vistorias
{
    public interface IVistoriasNegocio
    {
        List<VistoriasModel> ObterLista();
        void Inserir(VistoriasModel model);
        void Alterar(VistoriasModel model);
        VistoriasModel Obter(int id);
    }
}
