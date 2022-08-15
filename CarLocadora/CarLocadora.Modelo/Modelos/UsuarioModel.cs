using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class UsuarioModel : EnderecoModel
    {
        [Key][Required(ErrorMessage = "Campo obrigatório!")][StringLength(14, ErrorMessage = "Maxímo de 14 caracteres.")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string CPF { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(50, ErrorMessage = "Maxímo de 50 caracteres.")] public string RG { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(150, ErrorMessage = "Maxímo de 150 caracteres.")] public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.Date)] public DateTime DataNascimento { get; set; }
        [StringLength(15, ErrorMessage = "Maxímo de 15 caracteres.")] public string? Telefone { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(15, ErrorMessage = "Maxímo de 15 caracteres.")] public string Celular { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public bool Ativo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)] public DateTime? DataAlteracao { get; set; }
    }
}
