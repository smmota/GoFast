using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoFast.API.Models
{
    public class Motorista : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        [ForeignKey("Endereco")]
        public Guid IdEnderecoRefMotorista { get; set; }

        [Required]
        public Endereco Endereco { get; set; }

        [ForeignKey("Carro")]
        public Guid IdCarroRefMotorista { get; set; }

        [Required]
        public Carro Carro { get; set; }

        public Motorista(Guid id, string nome, string email, DateTime nascimento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Nascimento = nascimento;
            Endereco = new Endereco(new Guid(), "teste", 2, "teste", "teste", "teste", "teste", "teste");
            Carro = new Carro(new Guid(), "teste", "teste", DateTime.Now, new DocumentoCarro(new Guid(), Enums.TipoDocumentoEnum.Renavam, "teste", DateTime.Now, new Guid(), DateTime.Now));
        }
    }
}
