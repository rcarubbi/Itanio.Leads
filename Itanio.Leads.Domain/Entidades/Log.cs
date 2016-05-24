using System;

namespace Itanio.Leads.Domain.Entidades
{
    public class Log
    {
        public int Id { get; set; }

        public string EstadoAntigo { get; set; }

        public string EstadoNovo { get; set; }

        public DateTime DataHora { get; set; }

        public Usuario Usuario { get; set; }

        public int IdEntitdade { get; set; }

        public string Tipo { get; set; }
    }
}
