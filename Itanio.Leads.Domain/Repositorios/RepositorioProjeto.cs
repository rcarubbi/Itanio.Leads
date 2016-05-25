using System;
using Itanio.Leads.Domain.Entidades;
using System.Collections.Generic;
using System.Linq;
namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioProjeto
    {
        private IContexto _contexto;
        public RepositorioProjeto(IContexto contexto)
        {
            _contexto = contexto;
        }

        public Projeto ObterPorId(int? id)
        {
            return _contexto.Set<Projeto>().Find(id);
        }

        public void Atualizar(Projeto projetoAlterado)
        {
            var projetoAtual = ObterPorId(projetoAlterado.Id);

            _contexto.Atualizar(projetoAtual, projetoAlterado);
            _contexto.Salvar();
        }

        public void Adicionar(Projeto novoProjeto)
        {
            _contexto.Set<Projeto>().Add(novoProjeto);
            _contexto.Salvar();
        }

        public void Excluir(Projeto projeto)
        {
            _contexto.Set<Projeto>().Remove(projeto);
            _contexto.Salvar();
        }

        public List<Projeto> ListarAtivos()
        {
            return _contexto.Set<Projeto>().Where(p => p.Ativo).ToList();
        }
    }
}
