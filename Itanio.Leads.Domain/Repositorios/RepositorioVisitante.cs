using System;
using System.Linq;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioVisitante
    {
        private readonly IContexto _contexto;

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

        public Visitante ObterPorIdentificador(string guid)
        {
            return _contexto.Set<Visitante>().SingleOrDefault(v => v.Identificadores.Any(i => i.Guid == guid));
        }

        public void Atualizar(Visitante visitante)
        {
            var visitanteAtual = ObterPorId(visitante.Id);
            _contexto.Atualizar(visitanteAtual, visitante);
            _contexto.Salvar();
        }

        private Visitante ObterPorId(Guid id)
        {
            return _contexto.Set<Visitante>()
                .SingleOrDefault(v => v.Id == id);
        }
    }
}