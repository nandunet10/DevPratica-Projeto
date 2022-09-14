using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarLocadora.Modelo.Modelos
{
    public class CategoriaModel
    {
        [Key][Required(ErrorMessage = "Campo obrigatório!")][DatabaseGenerated(DatabaseGeneratedOption.Identity)] public int Id { get; set; } = 0;
        [Required(ErrorMessage = "Campo obrigatório!")][StringLength(100, ErrorMessage = "Maxímo de 100 caracteres.")][Display(Name = "Descrição do Serviço")] public string Descricao { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][Column(TypeName = "decimal(18, 2)")][Display(Name = "Valor diario")] public decimal ValorDiaria { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")] public bool Ativo { get; set; }
        [Required(ErrorMessage = "Campo obrigatório!")][DataType(DataType.DateTime)][Display(Name = "Data inclusão")] public DateTime DataInclusao { get; set; }
        [DataType(DataType.DateTime)][Display(Name = "Data alteração")] public DateTime? DataAlteracao { get; set; }

    }
}
