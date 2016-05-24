using Itanio.Leads.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioParametro
    {
        private IContexto _contexto;
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
