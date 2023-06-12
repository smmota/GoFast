using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoFast.API.Models
{
    public class Motorista : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public DateTime Nascimento { get; set; }

        public Guid IdEnderecoRefMotorista { get; set; }

        [Required]
        [ForeignKey("IdEnderecoRefMotorista")]
        public Endereco Endereco { get; set; }

        public Guid IdCarroRefMotorista { get; set; }

        [Required]
        [ForeignKey("IdCarroRefMotorista")]
        public Carro Carro { get; set; }

        public Motorista(Guid id, string nome, string email, DateTime nascimento, Endereco _endereco, Carro _carro)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Nascimento = nascimento;
            Endereco = _endereco;//new Endereco(new Guid(), "teste", 2, "teste", "teste", "teste", "teste", "teste");
            Carro = _carro;//new Carro(new Guid(), "teste", "teste", DateTime.Now, new DocumentoCarro(new Guid(), Enums.TipoDocumentoEnum.Renavam, "teste", DateTime.Now, new Guid(), DateTime.Now));
        }

        public Motorista()
        {

        }
    }
}
