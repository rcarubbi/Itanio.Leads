﻿using Itanio.Leads.Domain;
using Itanio.Leads.WebUI.Models;
using Itanio.Leads.WebUI.Servicos;
using System.Web.Http;
using System.Web.Http.Description;
using System.Linq;
using System;
using Itanio.Leads.Domain.Repositorios;

namespace Itanio.Leads.WebUI.Api
{
    public class VisitanteController : BaseApiController
    {
        public VisitanteController(IContexto contexto)
            : base(contexto)
        {

        }

        // POST: api/Visitante
        [ResponseType(typeof(VisitanteViewModel))]
        public IHttpActionResult Post(VisitanteViewModel viewModel)
        {
            if (string.IsNullOrWhiteSpace(viewModel.Email))
            {
                return NotFound();
            }

            ServicoVisitante visitanteServ = new ServicoVisitante(_contexto);
            var visitante = visitanteServ.CriarVisitante(viewModel.Nome, viewModel.Email, viewModel.Guid, new Guid(viewModel.IdProjeto), new Guid(viewModel.IdArquivo));
            viewModel.Guid = visitante.Identificadores.Last().Guid;
            return CreatedAtRoute("DefaultApi", new { id = visitante.Id }, viewModel);
        }

        [ResponseType(typeof(VisitanteViewModel))]
        public IHttpActionResult Get(string id)
        {
            RepositorioVisitante visitanteRepo = new RepositorioVisitante(_contexto);
            var entidade = visitanteRepo.ObterPorIdentificador(id);
            if (entidade == null)
                return NotFound();
            else
            {
                var visitante = VisitanteViewModel.FromEntity(entidade);
                return Ok(visitante);
            }
        }

    }
}
