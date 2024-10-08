﻿using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using Itanio.Leads.WebUI.Models;
using Itanio.Leads.WebUI.Servicos;

namespace Itanio.Leads.WebUI.Api
{
    public class AcessoController : BaseApiController
    {
        public AcessoController(IContexto contexto)
            : base(contexto)
        {
        }

        // POST api/values
        public IHttpActionResult Post([FromBody] AcessoViewModel acesso)
        {
            acesso.IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            var projetoRepo = new RepositorioProjeto(_contexto);

            var projeto = projetoRepo.ObterPorId(new Guid(acesso.IdProjeto));
            var arquivo = projeto.Arquivos.Single(a => a.Id == new Guid(acesso.IdArquivo));
            var visitanteRepo = new RepositorioVisitante(_contexto);

            var visitanteServ = new ServicoVisitante(_contexto);
            var visitante = visitanteServ.ObterVisitante(acesso.Email);
            var visitanteGuid = visitanteServ.ObterVisitante(new Guid(acesso.Guid));


            if (visitante != null && !visitante.Identificadores.Any(i => i.Guid == acesso.Guid))
            {
                string guid;
                if (visitanteGuid != null && visitanteGuid.Id != visitante.Id)
                {
                    guid = Guid.NewGuid().ToString();
                    acesso.Guid = guid;
                }
                else
                {
                    guid = acesso.Guid;
                }

                visitante.Identificadores.Add(new IdentificadorVisitante
                {
                    Guid = guid,
                    Ativo = true,
                    DataHora = DateTime.Now
                });
            }
            else if (visitante == null && !string.IsNullOrWhiteSpace(acesso.Email))
            {
                visitante = visitanteServ.CriarVisitante("Não informado", acesso.Email, acesso.Guid,
                    new Guid(acesso.IdProjeto), new Guid(acesso.IdArquivo));
                acesso.Guid = visitante.Identificadores.Last().Guid;
            }

            var acessoRepo = new RepositorioAcesso(_contexto);
            var acessoEntity = acesso.ToEntity(visitante, arquivo, projeto);
            acessoRepo.Gravar(acessoEntity);

            return CreatedAtRoute("DefaultApi", new {id = acessoEntity.Id}, acesso);
        }
    }
}