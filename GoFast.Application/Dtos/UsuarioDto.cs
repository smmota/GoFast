﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Application.Dtos
{
    public class UsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string LoginUser { get; set; }
        public string Senha { get; set; }
        public string Role { get; set; }
        public bool Ativo { get; set; }
    }
}
