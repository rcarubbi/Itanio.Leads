using System;

namespace Itanio.Leads.Domain.Entidades
{
    public class IdentificadorVisitante : Entidade
    {
        public string Guid { get; set; }

        public DateTime DataHora { get; set; }
    }
}