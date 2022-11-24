using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class ClienteModel : EnderecoModel
    {
        [Key][Required][StringLength(14, MinimumLength = 14, ErrorMessage = "Maxímo de 14 e minimo de 14 caracteres, ")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string CPF { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(12, MinimumLength = 12, ErrorMessage = "Maxímo de 12 e minimo de 12 caracteres.")] public string CNH { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(150, MinimumLength = 5, ErrorMessage = "Maxímo de 150 e minimo de 5 caracteres.")] public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.Date)][Display(Name = "Data nascimento")] public DateTime DataNascimento { get; set; }
        [StringLength(15, MinimumLength = 14, ErrorMessage = "Maxímo de 15 caracteres")] public string? Telefone { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(15, MinimumLength = 14, ErrorMessage = "Maxímo de 15 caracteres.")] public string Celular { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public bool Ativo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)][Display(Name = "Data inclusão")] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)][Display(Name = "Data alteração")] public DateTime? DataAlteracao { get; set; }
        [StringLength(100, ErrorMessage = "Maxímo de 100 caracteres")][Display(Name = "E-mail")] public string? Email { get; set; }
        [Display(Name = "E-mail Enviado")] public bool EmailEnviado { get; set; } = false;

    }
}

