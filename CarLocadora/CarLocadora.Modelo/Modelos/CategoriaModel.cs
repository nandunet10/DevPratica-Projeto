using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class CategoriaModel
    {
        [Key][Required][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; }
        [Required][StringLength(100)] public string Descricao { get; set; }
        [Required] public decimal ValorDiaria { get; set; }
        [Required] public bool Ativo { get; set; }
        [Required] public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }
}
