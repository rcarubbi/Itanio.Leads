using System.Web.Http;
using Itanio.Leads.Domain;

namespace Itanio.Leads.WebUI.Api
{
    public class BaseApiController : ApiController
    {
        protected IContexto _contexto;

        public BaseApiController(IContexto contexto)
        {
            _contexto = contexto;
        }
    }
}