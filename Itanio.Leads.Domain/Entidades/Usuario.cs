using System.Collections.Generic;

namespace Itanio.Leads.Domain.Entidades
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }

        public string Email { get; set; }

        public string Guid { get; set; }

        public virtual ICollection<Projeto> Projetos { get; set; }
    }
}