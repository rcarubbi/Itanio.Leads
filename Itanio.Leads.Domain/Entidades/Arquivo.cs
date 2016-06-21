using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.Domain.Entidades
{
    public class Arquivo : Entidade
    {
        public virtual Projeto Projeto { get; set; }

        public string NomeArquivo { get; set;  }

        public string Url { get; set; }


        public virtual ICollection<Acesso> Acessos { get; set; }
    }
}
