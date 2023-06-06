using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models
{
    public class DocumentoCarro : Documento
    {
        [Required]
        public DateTime Renovacao { get; set; }
    }
}
