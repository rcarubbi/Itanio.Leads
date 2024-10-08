﻿using System.Collections.Generic;
using System.Linq;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioAcesso
    {
        private readonly IContexto _contexto;

        public RepositorioAcesso(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void Gravar(Acesso acesso)
        {
            _contexto.Set<Acesso>().Add(acesso);
            _contexto.Salvar();
        }

        public List<Acesso> ListarAnonimosPorGuid(string guid)
        {
            return _contexto.Set<Acesso>().Where(a => a.Guid == guid && a.Visitante == null).ToList();
        }
    }
}