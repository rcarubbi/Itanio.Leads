using System.Collections.Generic;

namespace Itanio.Leads.Domain.Entidades
{
    public class Arquivo : Entidade
    {
        public virtual Projeto Projeto { get; set; }

        public string NomeArquivo { get; set; }

        public string Url { get; set; }

        public string Descricao { get; set; }

        public virtual ICollection<Acesso> Acessos { get; set; }
    }
}