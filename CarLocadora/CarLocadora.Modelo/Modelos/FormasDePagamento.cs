using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Modelo.Modelos
{
    public class FormasDePagamento
    {
        [Key][Required] public int Placa { get; set; }
        [Required][StringLength(150)] public string Descricao { get; set; }
        [Required] public bool Ativo { get; set; }
        [Required] public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
