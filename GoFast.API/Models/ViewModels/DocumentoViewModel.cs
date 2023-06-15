using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoFast.API.Models.Enums;

namespace GoFast.API.Models.ViewModels
{
    public class DocumentoViewModel
    {
        public TipoDocumentoEnum TipoDocumento { get; set; }

        public string Numero { get; set; }

        public string Expedicao { get; set; }

        public Guid BlobId { get; set; }

        public BlobStorageViewModel BlobStorage { get; set; }
    }
}
