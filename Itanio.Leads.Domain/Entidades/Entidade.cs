using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.Domain.Entidades
{
    public abstract class Entidade
    {
        public int Id { get; set; }

        public bool Ativo { get; set; }
    }
}
