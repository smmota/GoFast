using GoFast.API.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace GoFast.API.Models.InputModel
{
    public class MotoristaInputModel
    {
        [Required]
        [MaxLength(150)]
        public string Nome { get; set; }

        [Required]
        [MaxLength(50)]
        public string Email { get; set; }

        [Required]
        public string Nascimento { get; set; }

        [Required]
        public EnderecoInputModel Endereco { get; set; }

        [Required]
        public CarroInputModel Carro { get; set; }

        [Required]
        public BlobInputModel Blob { get; set; }
    }
}
