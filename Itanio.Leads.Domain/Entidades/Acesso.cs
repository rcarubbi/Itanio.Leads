using System;

namespace Itanio.Leads.Domain.Entidades
{
    public class Acesso : Entidade
    {
        public DateTime DataHoraAcesso { get; set; }

        public virtual Visitante Visitante { get; set; }

        public virtual Arquivo Arquivo { get; set; }

        public TipoNavegador TipoNavegador {get;set;}

        public string UserAgent { get; set; }           
    }
}