using Itanio.Leads.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioVisitante
    {
        private IContexto _contexto;
        public RepositorioVisitante(IContexto contexto)
        {
            _contexto = contexto;
        }

        public Visitante ObterPorEmail(string email)
        {
            return _contexto.Set<Visitante>().SingleOrDefault(v => v.Email == email);
        }

        public void Adicionar(Visitante visitante)
        {
            _contexto.Set<Visitante>().Add(visitante);
            _contexto.Salvar();
        }
    }
}
