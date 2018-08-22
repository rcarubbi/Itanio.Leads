using System;
using System.IO;
using System.Web.Http;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Repositorios;

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
            var arquivoRepo = new RepositorioArquivo(_contexto);
            var arquivo = arquivoRepo.ObterPorId(id);
            return Path.Combine(arquivo.Url, arquivo.NomeArquivo);
        }

        [Route("api/Arquivo/GetDescricao", Name = "GetDescricao")]
        public string GetDescricao(Guid id)
        {
            var arquivoRepo = new RepositorioArquivo(_contexto);
            var arquivo = arquivoRepo.ObterPorId(id);
            return arquivo.Descricao;
        }
    }
}