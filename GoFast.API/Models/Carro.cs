using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoFast.API.Models
{
    public class Carro : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; private set; } = Guid.NewGuid();

        [Required]
        [MaxLength(7)]
        public string Placa { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; }

        [Required]
        public DateTime AnoFabricacao { get; set; }

        [ForeignKey("DocumentoCarro")]
        public Guid IdDocumentoRefCarro { get; set; }

        public DocumentoCarro DocumentoCarro { get; set; }


        public Carro()
        {
        }

        public Carro(Guid id, string placa, string modelo, DateTime anoFabricacao, DocumentoCarro documentoCarro)
        {
            Id = id;
            Placa = placa;
            Modelo = modelo;
            AnoFabricacao = anoFabricacao;
            this.DocumentoCarro = documentoCarro;
        }
    }
}
