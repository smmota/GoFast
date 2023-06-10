using GoFast.API.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.InputModel
{
    public class DocumentoCarroInputModel : DocumentoInputModel
    {
        [Required]
        public DateTime Renovacao { get; set; }
    }
}