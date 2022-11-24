using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Comum.Modelos
{
    public class Seguro
    {
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(14, MinimumLength = 14, ErrorMessage = "Maxímo de 14 caracteres.")] public string CPF { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(12, MinimumLength = 12, ErrorMessage = "Maxímo de 12 caracteres.")] public string CNH { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(150, MinimumLength = 5, ErrorMessage = "Maxímo de 150 caracteres.")] public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.Date)][Display(Name = "Data nascimento")] public DateTime DataNascimento { get; set; }
        [StringLength(15, ErrorMessage = "Maxímo de 15 caracteres")] public string? Telefone { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(8, MinimumLength = 7, ErrorMessage = "Maxímo de 8 caracteres.")] public string Placa { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(100, MinimumLength = 4, ErrorMessage = "Máximo de 100 caracteres.")] public string Marca { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(150, MinimumLength = 4, ErrorMessage = "Maxímo de 150 caracteres.")] public string Modelo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(100, MinimumLength = 3, ErrorMessage = "Maxímo de 100 caracteres.")] public string Combustivel { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][Display(Name = "Data retirada")] public DateTime DataHoraRetiradaPrevista { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][Display(Name = "Data devolução")] public DateTime DataHoraDevolucaoPrevista { get; set; }

    }
}

