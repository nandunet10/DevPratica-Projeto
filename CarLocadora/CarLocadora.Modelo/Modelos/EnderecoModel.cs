using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Modelo.Modelos;

public class EnderecoModel
{
    [Required(ErrorMessage = "Campo obrigatório!")][StringLength(50)] public string Logradouro { get; set; } 
    [Required(ErrorMessage = "Campo obrigatório!")][StringLength(20)] public string Numero { get; set; } 
    [StringLength(50, ErrorMessage ="Maxímo de 50 caracteres.")] public string? Complemento { get; set; }
    [Required(ErrorMessage = "Campo obrigatório!")][StringLength(50)] public string Cidade { get; set; } 
    [Required(ErrorMessage = "Campo obrigatório!")][StringLength(2)] public string Estado { get; set; } 

}
