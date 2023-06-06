using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.ViewModels
{
    public class CarroViewModel
    {
        [Required]
        [MaxLength(7)]
        public string Placa { get; set; }
        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; }
        [Required]
        public DateTime AnoFabricacao { get; set; }
        [Required]
        public DocumentoCarroViewModel DocumentoCarro { get; set; }
    }
}
