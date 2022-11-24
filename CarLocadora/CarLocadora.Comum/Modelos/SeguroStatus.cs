using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Comum.Modelos
{
    public class SeguroStatus
    {
        [Required(ErrorMessage = "Campo obrigatório!")] public Int32 Protocolo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(1, MinimumLength = 1, ErrorMessage = "Minimo de 1 caracteres.")] public string Cliente { get; set; }
        [Display(Name = "Apolice")] public Guid? Apolice { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(1, MinimumLength = 1, ErrorMessage = "Minimo de 1 caracteres.")] public string Status { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(1, MinimumLength = 1, ErrorMessage = "Minimo de 1 caracteres.")] public string Observacao { get; set; }

    }
}

