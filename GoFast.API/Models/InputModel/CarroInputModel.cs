using GoFast.API.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.InputModel
{
    public class CarroInputModel
    {
        [Required]
        [MaxLength(7)]
        public string Placa { get; set; }

        [Required]
        [MaxLength(50)]
        public string Modelo { get; set; }

        [Required]
        public DateTime AnoFabricacao { get; set; }

        public DocumentoCarroInputModel DocumentoCarro { get; set; }
    }
}