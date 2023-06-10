using GoFast.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.InputModel
{
    public class DocumentoInputModel
    {
        public TipoDocumentoEnum TipoDocumento { get; set; }

        [Required]
        [MaxLength(30)]
        public string Numero { get; set; }

        [Required]
        public DateTime Expedicao { get; set; }

        [Required]
        public Guid IdBlob { get; set; }
    }
}