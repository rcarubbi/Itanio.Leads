using Itanio.Leads.Domain;
using System.Web.Http;

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
