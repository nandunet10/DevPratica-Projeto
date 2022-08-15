using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class CategoriaModel
    {
        [Key][Required(ErrorMessage = "Campo obrigatório!")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(100, ErrorMessage = "Maxímo de 100 caracteres.")] public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][Column(TypeName = "decimal(18, 2)")] public decimal ValorDiaria { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public bool Ativo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)] public DateTime? DataAlteracao { get; set; }

    }
}
