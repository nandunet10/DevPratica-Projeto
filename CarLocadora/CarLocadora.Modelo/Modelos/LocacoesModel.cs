using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class LocacoesModel
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)][Required(ErrorMessage = "O Id é obrigatório.")] public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(14)] public string? ClienteCPF { get; set; }
        public ClienteModel? Cliente { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public int? FormasDePagamentoId { get; set; }
        public FormasDePagamentoModel? FormasDePagamento { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][Display(Name = "Data reserva")] public DateTime DataHoraReserva { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][Display(Name = "Data retirada")] public DateTime DataHoraRetiradaPrevista { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][Display(Name = "Data devolução")] public DateTime DataHoraDevolucaoPrevista { get; set; }
        [StringLength(8)] public string? VeiculoPlaca { get; set; }
        public VeiculoModel? Veiculo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)][Display(Name = "Data inclusão")] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)][Display(Name = "Data alteração")] public DateTime? DataAlteracao { get; set; }

        #region Reference Seguro
        [Display(Name = "Seguro Apólice")] public Guid? SeguroApolice { get; set; }
        [Display(Name = "Seguro Aprovado")] public bool? SeguroAprovado { get; set; }
        [Display(Name = "Seguro Observação")][StringLength(1000, ErrorMessage = "Este campo deve ter no máximo 1000 caracteres")] public string? SeguroObservacao { get; set; } = "";

        #endregion


    }
}
