using System;

namespace Itanio.Leads.Domain.Entidades
{
    public abstract class Entidade
    {
        public Guid Id { get; set; }

        public bool Ativo { get; set; }
    }
}