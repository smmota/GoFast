using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.ViewModels
{
    public class DocumentoCarroViewModel : DocumentoViewModel
    {
        [Required]
        public DateTime Renovacao { get; set; }
    }
}
