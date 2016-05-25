﻿using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using Itanio.Leads.WebApi.Models;
using Itanio.Leads.WebApi.Servicos;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Itanio.Leads.WebApi.Controllers
{
    public class AcessoController : BaseApiController
    {
        public AcessoController(IContexto contexto)
            : base(contexto)
        {

        }

        // POST api/values
        public void Post([FromBody]AcessoViewModel acesso)
        {
            acesso.IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString();
            
            RepositorioProjeto projetoRepo = new RepositorioProjeto(_contexto);

            Projeto projeto = projetoRepo.ObterPorId(acesso.IdProjeto);
            Arquivo arquivo = projeto.Arquivos.Single(a => a.Id == acesso.IdArquivo);

            ServicoVisitante visitanteServ = new ServicoVisitante(_contexto);
            Visitante visitante = visitanteServ.ObterVisitante(acesso.Email);

            RepositorioAcesso acessoRepo = new RepositorioAcesso(_contexto);
            acessoRepo.Gravar(acesso.ToEntity(visitante, arquivo, projeto));
        }

    

    
    }
}