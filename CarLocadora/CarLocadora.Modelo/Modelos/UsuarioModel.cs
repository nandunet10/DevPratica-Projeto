using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class UsuarioModel : EnderecoModel
    {
        [Key][Required][StringLength(14)][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string CPF { get; set; }
        [Required][StringLength(50)] public string RG { get; set; }
        [Required][StringLength(150)] public string Nome { get; set; }
        [Required] public DateTime DataNascimento { get; set; }
        [StringLength(15)] public string? Telefone { get; set; }
        [Required][StringLength(15)] public string Celular { get; set; }
        [Required] public bool Ativo { get; set; }
        [Required] public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
