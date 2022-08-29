using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Modelos
{
    public class LocacaoModel
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)][Required(ErrorMessage = "O Id é obrigatório.")] public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(14)] public string ClienteCPF { get; set; }
        public ClienteModel Cliente { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public int FormaDePagamentoId { get; set; }
        public FormasDePagamentoModel FormasDePagamento { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public int CategoriaId { get; set; }
        public CategoriaModel CategoriaModel { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public DateTime DataHoraReserva { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public DateTime DataHoraRetiradaPrevista { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public DateTime DataHoraDevolucaoPrevista { get; set; }
        [StringLength(8)] public string VeiculoPlaca { get; set; }
        public VeiculoModel Veiculo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)] public DateTime? DataAlteracao { get; set; }
    }
}
