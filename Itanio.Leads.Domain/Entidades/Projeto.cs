using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.Domain.Entidades
{
    public class Projeto : Entidade
    {
        public virtual ICollection<Arquivo> Arquivos { get; set; }

        public virtual ICollection<Acesso> Acessos { get; set; }

        public string Nome { get; set; }

        public string UrlBase { get; set; }

        public string TemplateEmail { get; set; }
    }
}
