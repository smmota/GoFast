using GoFast.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models
{
    public class BlobStorage : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [MaxLength(200)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string base64 { get; set; }
        public string Link { get; set; }
        public string IdAzure { get; set; }
    }
}
