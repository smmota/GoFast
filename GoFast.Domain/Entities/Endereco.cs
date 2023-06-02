﻿using System.ComponentModel.DataAnnotations;

namespace GoFast.Domain.Entities
{
    public class Endereco : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Rua { get; set; }
        [Required]
        [Range(0, 20000)]
        public int Numero { get; set; }
        [Required]
        [MaxLength(8)]
        public string CEP { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string Cidade { get; set; }
        [Required]
        public string Estado { get; set; }
        [Required]
        public string Complemento { get; set; }
    }
}