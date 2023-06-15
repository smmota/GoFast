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
        public string Link { get; set; }

        [Required]
        public string Container { get; set; }

        public IEnumerable<Documento>? Documentos { get; set; }

        public BlobStorage(Guid id, string name, string base64)
        {
            Id = id;
            Name = name;
        }

        public BlobStorage()
        {
        }
    }
}
