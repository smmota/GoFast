using GoFast.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models
{
    public class DocumentoCarro : Documento
    {
        [Required]
        public DateTime Renovacao { get; set; }

        public DocumentoCarro()
        {
        }

        public DocumentoCarro(Guid id, TipoDocumentoEnum tipoDocumento, string numero, DateTime expedicao, Guid idBlob, DateTime renovacao, BlobStorage blob)
            : base(id, tipoDocumento, numero, expedicao, blob)
        {
            Renovacao = renovacao;
        }
    }
}
