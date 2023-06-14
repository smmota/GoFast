using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.ViewModels
{
    public class BlobStorageViewModel
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [Required]
        //public string base64 { get; set; }
        public string Link { get; set; }
        //public string IdAzure { get; set; }
    }
}
