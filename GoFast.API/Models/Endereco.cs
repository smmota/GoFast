using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models
{
    public class Endereco : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Rua { get; set; }
        [Required]
        [Range(0, 20000)]
        public int Numero { get; set; }
        [Required]
        [MaxLength(9)]
        public string CEP { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Complemento { get; set; }

        public Endereco(Guid id, string rua, int numero, string cEP, string bairro, string cidade, string estado, string complemento)
        {
            Id = id;
            Rua = rua;
            Numero = numero;
            CEP = cEP;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Complemento = complemento;
        }

        public Endereco()
        {
        }
    }
}
