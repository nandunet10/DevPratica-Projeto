using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Comum.Modelos
{
    public class LoginRequisicao
    {
        [Required]
        public string Usuario { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}
