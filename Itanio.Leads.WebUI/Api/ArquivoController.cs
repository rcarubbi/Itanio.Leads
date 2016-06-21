﻿using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Repositorios;
using System;
using System.IO;

namespace Itanio.Leads.WebUI.Api
{
    public class ArquivoController : BaseApiController
    {
        public ArquivoController(IContexto contexto)
            : base(contexto)
        {

        }

        public string Get(Guid id)
        {
            RepositorioArquivo arquivoRepo = new RepositorioArquivo(_contexto);
            var arquivo = arquivoRepo.ObterPorId(id);
            return Path.Combine(arquivo.Url, arquivo.NomeArquivo);
        }
    }
}
