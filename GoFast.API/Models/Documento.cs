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
        //[Required]
        
        public Guid BlobId { get; set; }

        [ForeignKey("BlobId")]
        public BlobStorage? BlobStorage { get; set; }

        public Documento()
        {
        }

        public Documento(Guid id, TipoDocumentoEnum tipoDocumento, string numero, DateTime expedicao, BlobStorage blob)
        {
            Id = id;
            TipoDocumento = tipoDocumento;
            Numero = numero;
            Expedicao = expedicao;
            //BlobId = blob.Id;
            BlobStorage = blob;
        }
    }
}
