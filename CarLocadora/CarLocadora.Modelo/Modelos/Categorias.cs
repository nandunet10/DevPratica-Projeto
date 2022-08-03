using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Modelo.Modelos
{
    public class Categorias
    {
        [Key][Required] public int Id { get; set; }
        [Required][StringLength(100)] public string Descricao { get; set; }
        [Required] public decimal ValorDiaria { get; set; }
        [Required] public bool Ativo { get; set; }
        [Required] public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
