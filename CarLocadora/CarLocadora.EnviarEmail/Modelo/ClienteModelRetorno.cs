namespace CarLocadora.EnviarEmail.Modelo
{
    public class ClienteModelRetorno
    {
        public string Cpf { get; set; }
        public string Cnh { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string Email { get; set; }
        public bool EmailEnviado { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

    }
}
