using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.ViewModels
{
    public class CarroViewModel
    {
        public string Placa { get; set; }

        public string Modelo { get; set; }

        public DateTime AnoFabricacao { get; set; }

        public DocumentoCarroViewModel DocumentoCarro { get; set; }
    }
}
