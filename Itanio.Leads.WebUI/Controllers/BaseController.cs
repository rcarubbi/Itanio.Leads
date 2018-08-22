using System.Web.Mvc;
using Itanio.Leads.Domain;

namespace Itanio.Leads.WebUI.Controllers
{
    public class BaseController : Controller
    {
        protected IContexto _contexto;

        public BaseController(IContexto contexto)
        {
            _contexto = contexto;
        }
    }
}