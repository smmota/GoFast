using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.InputModel
{
    public class BlobInputModel
    {
        [Required]
        public string ImagemBase64 { get; set; }
    }
}