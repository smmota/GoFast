using System.ComponentModel.DataAnnotations;

namespace GoFast.Domain.Entities
{
    public class DocumentoCarro : Documento
    {
        [Required]
        public DateTime Renovacao { get; set; }
    }
}
