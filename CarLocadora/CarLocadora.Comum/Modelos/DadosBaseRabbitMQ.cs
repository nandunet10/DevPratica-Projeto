namespace CarLocadora.Comum.Modelos
{
    public class DadosBaseRabbitMQ
    {
        public string HostName { get; set; } = "";
        public int Port { get; set; }
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
