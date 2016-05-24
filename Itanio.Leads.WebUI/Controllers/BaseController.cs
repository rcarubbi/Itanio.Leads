using Itanio.Leads.Domain;
using System.Web.Mvc;

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