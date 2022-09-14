namespace CarLocadora.Front.Servico
{
    public interface IApiToken
    {
        Task<string> Obter();
    }
}
