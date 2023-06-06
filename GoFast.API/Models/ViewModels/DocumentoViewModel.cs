using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using GoFast.API.Models.Enums;

namespace GoFast.API.Models.ViewModels
{
    public class DocumentoViewModel
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
