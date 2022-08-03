using System.ComponentModel.DataAnnotations;

namespace CarLocadora.Modelo.Modelos
{
    public class Veiculos
    {
        [Key][Required][StringLength(8)] public string Placa { get; set; }
        [StringLength(100)] public string? Chassi { get; set; }
        [Required][StringLength(100)] public string Marca { get; set; }
        [Required][StringLength(150)] public string Modelo { get; set; }
        [Required][StringLength(100)] public string Combustivel { get; set; }
        [Required][StringLength(100)] public string Cor { get; set; }
        [StringLength(2000)] public string? Opcionais { get; set; }
        [Required] public bool Ativo { get; set; }
        [Required] public DateTime DataInclusao { get; set; }
        public DateTime? DataAlteracao { get; set; }
    }
}
