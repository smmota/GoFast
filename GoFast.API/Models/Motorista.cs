using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoFast.API.Models
{
    public class Motorista : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }
        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }
        [ForeignKey("Motorista.Endereco")]
        [Required]
        public Endereco Endereco { get; set; }
        [ForeignKey("Motorista.Carro")]
        [Required]
        public Carro Carro { get; set; }
    }
}
