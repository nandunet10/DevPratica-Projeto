using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Modelo.Modelos;

public class EnderecoModel
{
    [Required][StringLength(50)] public string Logradouro { get; set; } 
    [Required][StringLength(20)] public string Numero { get; set; } 
    [StringLength(50)] public string? Complemento { get; set; }
    [Required][StringLength(50)] public string Cidade { get; set; } 
    [Required][StringLength(2)] public string Estado { get; set; } 

}
