﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.Domain.Entidades
{
    public class Visitante : Entidade
    {
        public Visitante()
        {
            Acessos = new List<Acesso>();
        }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Guid { get; set; }

        public virtual ICollection<Acesso> Acessos { get; set; }
    }
}
