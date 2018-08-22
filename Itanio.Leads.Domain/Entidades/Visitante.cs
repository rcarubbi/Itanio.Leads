using System.Collections.Generic;

namespace Itanio.Leads.Domain.Entidades
{
    public class Visitante : Entidade
    {
        public Visitante()
        {
            Acessos = new List<Acesso>();
            Identificadores = new List<IdentificadorVisitante>();
        }

        public string Nome { get; set; }

        public string Email { get; set; }

        public virtual ICollection<IdentificadorVisitante> Identificadores { get; set; }

        public virtual ICollection<Acesso> Acessos { get; set; }
    }
}