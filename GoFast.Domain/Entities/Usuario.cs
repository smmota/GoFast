﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoFast.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string LoginUser { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
    }
}
