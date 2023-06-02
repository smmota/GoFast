﻿using GoFast.API.Entities.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace GoFast.Domain.Entities
{
    public class Documento : IEntityBase
    {
        [Key]
        [Required]
        public Guid Id { get; private set; }
        [Required]
        public TipoDocumentoEnum TipoDocumento { get; set; }
        [Required]
        [MaxLength(30)]
        public string Numero { get; set; }
        [Required]
        public DateTime Expedicao { get; set; }
        [Required]
        [ForeignKey("Documento.Blob")]
        public BlobStorage Blob { get; set; }
    }
}