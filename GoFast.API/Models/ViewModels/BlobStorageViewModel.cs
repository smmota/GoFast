using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.ViewModels
{
    public class BlobStorageViewModel
    {
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Link { get; set; }
    }
}
