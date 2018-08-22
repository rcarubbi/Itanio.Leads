using System.Linq;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioParametro
    {
        private readonly IContexto _contexto;

        public RepositorioParametro(IContexto contexto)
        {
            _contexto = contexto;
        }

        public string ObterValorPorChave(string chave)
        {
            return _contexto.Set<Parametro>().Single(p => p.Chave == chave).Valor;
        }
    }
}