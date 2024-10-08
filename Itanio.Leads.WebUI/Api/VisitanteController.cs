﻿using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using Itanio.Leads.WebUI.Models;
using Itanio.Leads.WebUI.Servicos;

namespace Itanio.Leads.WebUI.Api
{
    public class VisitanteController : BaseApiController
    {
        public VisitanteController(IContexto contexto)
            : base(contexto)
        {
        }

        [HttpPost]
        [Route("api/Visitante/CriarContato", Name = "CriarContato")]
        [ResponseType(typeof(VisitanteViewModel))]
        public IHttpActionResult CriarContato(AcessoViewModel viewModel)
        {
            var servicoVisitante = new ServicoVisitante(_contexto);
            var visitante = servicoVisitante.ObterVisitante(viewModel.Email);
            if (visitante != null) return BadRequest();

            var projetoRepo = new RepositorioProjeto(_contexto);
            var projeto = projetoRepo.ObterPorId(new Guid(viewModel.IdProjeto));

            visitante = new Visitante
            {
                Nome = "Visitante",
                Email = viewModel.Email,
                Ativo = true
            };

            var guidIdentificador = Guid.NewGuid().ToString();
            visitante.Identificadores.Add(new IdentificadorVisitante
            {
                DataHora = DateTime.Now,
                Guid = guidIdentificador,
                Ativo = true
            });

            var visitanteRepo = new RepositorioVisitante(_contexto);
            visitanteRepo.Adicionar(visitante);


            var acessoRepo = new RepositorioAcesso(_contexto);
            viewModel.IP = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            acessoRepo.Gravar(viewModel.ToEntity(visitante, null, projeto));

            var visitanteViewModel = VisitanteViewModel.FromEntity(visitante);
            return CreatedAtRoute("CriarContato", new {id = visitante.Id.ToString()}, visitanteViewModel);
        }

        // POST: api/Visitante
        [ResponseType(typeof(VisitanteViewModel))]
        public IHttpActionResult Post(VisitanteViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Email)) return NotFound();

            var visitanteServ = new ServicoVisitante(_contexto);
            var visitante = visitanteServ.CriarVisitante(viewModel.Nome, viewModel.Email, viewModel.Guid,
                new Guid(viewModel.IdProjeto), new Guid(viewModel.IdArquivo));
            viewModel.Guid = visitante.Identificadores.Last().Guid;
            return CreatedAtRoute("DefaultApi", new {id = visitante.Id}, viewModel);
        }

        [ResponseType(typeof(VisitanteViewModel))]
        public IHttpActionResult Get(string id)
        {
            var visitanteRepo = new RepositorioVisitante(_contexto);
            var entidade = visitanteRepo.ObterPorIdentificador(id);
            if (entidade == null)
            {
                return NotFound();
            }

            var visitante = VisitanteViewModel.FromEntity(entidade);
            return Ok(visitante);
        }
    }
}