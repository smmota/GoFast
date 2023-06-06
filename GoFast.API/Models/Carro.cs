using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoFast.API.Models
{
    public class Carro : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }
        [Required]
        [MaxLength(7)]
        public string Placa { get; set; }
        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; }
        [Required]
        public DateTime AnoFabricacao { get; set; }
        [Required]
        [ForeignKey("Carro.DocumentoCarro")]
        public DocumentoCarro DocumentoCarro { get; set; }
    }
}
