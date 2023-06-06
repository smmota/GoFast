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

        public DocumentoCarro(Guid id, TipoDocumentoEnum tipoDocumento, string numero, DateTime expedicao, Guid idBlob, DateTime renovacao)
            : base(id, tipoDocumento, numero, expedicao, idBlob)
        {
            Renovacao = renovacao;
        }
    }
}
