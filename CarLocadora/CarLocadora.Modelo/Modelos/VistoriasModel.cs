using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarLocadora.Modelo.Modelos
{
    public class VistoriasModel
    {
        [Key][Required(ErrorMessage = "Campo obrigatório!")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Campo obrigatório!")] public int LocacaoId { get; set; }
        public LocacaoModel Locacao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public long KmSaida { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(50, ErrorMessage = "Maxímo de 50")] public string CombustivelSaida { get; set; }
        [StringLength(2000, ErrorMessage = "Maxímo de 2000")] public string ObservacaoSaida { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public DateTime DataHoraRetiradaPatio { get; set; }
        public long KmEntrada { get; set; }
        [StringLength(50, ErrorMessage = "Maxímo de 50")] public string CombustivelEntrada { get; set; }
        [StringLength(2000, ErrorMessage = "Maxímo de 2000")] public string ObservacaoEntrada { get; set; }
        public DateTime DataHoraDevolucaoPatio { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)] public DateTime? DataAlteracao { get; set; }

    }
}
