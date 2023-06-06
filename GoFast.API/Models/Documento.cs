using GoFast.API.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GoFast.API.Models
{
    public class Documento : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }
        //[Required]
        public TipoDocumentoEnum TipoDocumento { get; set; }
        [Required]
        [MaxLength(30)]
        public string Numero { get; set; }
        [Required]
        public DateTime Expedicao { get; set; }
        [Required]
        [ForeignKey("BlobStorage")]
        public Guid IdBlob { get; set; }

        public Documento()
        {
        }

        public Documento(Guid id, TipoDocumentoEnum tipoDocumento, string numero, DateTime expedicao, Guid idBlob)
        {
            Id = id;
            TipoDocumento = tipoDocumento;
            Numero = numero;
            Expedicao = expedicao;
            IdBlob = idBlob;
        }
    }
}
