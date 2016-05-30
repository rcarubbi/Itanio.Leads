using Itanio.Leads.Api.Models;
using Itanio.Leads.Api.Servicos;
using Itanio.Leads.Domain;
using System.Web.Http;
using System.Web.Http.Description;

namespace Itanio.Leads.Api.Controllers
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
            ServicoVisitante visitanteServ = new ServicoVisitante(_contexto);
            var visitante = visitanteServ.CriarVisitante(viewModel.Nome, viewModel.Email, viewModel.Guid, viewModel.IdProjeto, viewModel.IdArquivo);
            return CreatedAtRoute("DefaultApi", new { id = visitante.Id }, viewModel);
        }

    
    
    }
}
