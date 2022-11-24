using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Comum.Modelos
{
    public class LocacoesSeguro
    {
        [Required(ErrorMessage = "O Id é obrigatório.")] public int Id { get; set; }
        [Display(Name = "Seguro Apólice")] public Guid? SeguroApolice { get; set; }
        [Display(Name = "Seguro Aprovado")] public bool? SeguroAprovado { get; set; }
        [Display(Name = "Seguro Observação")] public string? SeguroObservacao { get; set; }

    }
}
