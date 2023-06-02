using GoFast.API.Entities;
using System.Buffers.Text;
using System.ComponentModel.DataAnnotations;

namespace GoFast.Domain.Entities
{
    public class BlobStorage : IEntityBase
    {
        [key]
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