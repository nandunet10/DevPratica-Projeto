using CarLocadora.Modelo.Modelos;

namespace CarLocadora.Negocio.RabbitMQ
{
    public interface IMensageriaNegocio
    {
        void PublicarMensagem(object obj, string exchange = "", string fila = "");
    }
}
