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

        public BlobStorage(Guid id, string name, string base64)
        {
            Id = id;
            Name = name;
            this.base64 = base64;
        }

        public BlobStorage()
        {
        }
    }
}
