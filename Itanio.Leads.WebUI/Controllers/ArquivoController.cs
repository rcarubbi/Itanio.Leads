using System;
using System.Web.Mvc;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using Itanio.Leads.WebUI.ActionResults;

namespace Itanio.Leads.WebUI.Controllers
{
    [AllowAnonymous]
    public class ArquivoController : BaseController
    {
        public ArquivoController(IContexto contexto)
            : base(contexto)
        {
        }

        // GET: Arquivo
        public ActionResult Index(string id, string guid)
        {
            var arquivoRepo = new RepositorioArquivo(_contexto);
            var arquivo = arquivoRepo.ObterPorId(new Guid(id));
            var visitanteRepo = new RepositorioVisitante(_contexto);
            var visitante = visitanteRepo.ObterPorIdentificador(guid);
            ViewBag.Url = arquivo.Projeto.UrlBase + "/" + arquivo.Projeto.LandPage + $"?IdArquivo={arquivo.Id}";

            var acesso = new Acesso();
            acesso.IP = HttpContext.Request.ServerVariables["REMOTE_ADDR"];
            var projeto = arquivo.Projeto;
            var acessoRepo = new RepositorioAcesso(_contexto);

            acesso.Visitante = visitante;
            acesso.Projeto = projeto;
            acesso.Arquivo = arquivo;
            acesso.DataHoraAcesso = DateTime.Now;
            acesso.Ativo = true;
            acesso.Guid = guid;
            acesso.UserAgent = HttpContext.Request.UserAgent;

            if (visitante == null)
            {
                acesso.Url = "ArquivoNaoDisponivel";
                acessoRepo.Gravar(acesso);
                return View("ArquivoNaoDisponivel");
            }

            acesso.Url = $"{arquivo.Url}/{arquivo.NomeArquivo}";
            acessoRepo.Gravar(acesso);

            return new ExternalFileResult(acesso.Url);
        }
    }
}