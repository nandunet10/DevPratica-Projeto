using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class VeiculoModel
    {
        [Key][Required(ErrorMessage = "Campo obrigatório!")][StringLength(8, ErrorMessage = "Maxímo de 8 caracteres.")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public string Placa { get; set; }
        [StringLength(100, ErrorMessage = "Maxímo de 100 caracteres.")] public string? Chassi { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(100, ErrorMessage = "Máximo de 100 caracteres.")] public string Marca { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(150, ErrorMessage = "Maxímo de 150 caracteres.")] public string Modelo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(100, ErrorMessage = "Maxímo de 100 caracteres.")] public string Combustivel { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(100, ErrorMessage = "Maxímo de 100 caracteres.")] public string Cor { get; set; }
        [StringLength(2000, ErrorMessage = "Maxímo de 2000 caracteres")] public string? Opcionais { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public bool Ativo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)] public DateTime? DataAlteracao { get; set; }
    }
}
