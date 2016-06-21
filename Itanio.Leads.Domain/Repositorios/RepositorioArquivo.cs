using Itanio.Leads.Domain.Entidades;
using System;
using System.Linq;

namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioArquivo
    {
        private IContexto _contexto;
        public RepositorioArquivo(IContexto contexto)
        {
            _contexto = contexto;
        }

        public Arquivo ObterPorId(Guid id)
        {
            return _contexto.Set<Arquivo>().Single(a => a.Id == id);
        }
    }
}
