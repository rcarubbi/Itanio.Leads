using Itanio.Leads.DataAccess;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Itanio.Leads.WebUI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
#if DEBUG
            var dominios = "*";
#else
            // Web API configuration and services
            var dominios = ObterDominios();
            if (string.IsNullOrEmpty(dominios))
            {
                dominios = "*";
            }
#endif
            var cors = new EnableCorsAttribute(dominios, "*", "*");
            config.EnableCors(cors);
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

        }

        private static string ObterDominios()
        {
            IContexto contexto = new Contexto();
            RepositorioProjeto projetoRepo = new RepositorioProjeto(contexto);

            var urls = projetoRepo.ListarAtivos().Select(x =>
            {
                var uri = new Uri(x.UrlBase);
                if (uri.Port == 80)
                {
                    return string.Format("{0}://{1}", uri.Scheme, uri.Host);
                }
                else
                {
                    return string.Format("{0}://{1}:{2}", uri.Scheme, uri.Host, uri.Port);
                }
            });
            var urlsSemWWW = new List<String>();
            urls.ToList().ForEach(x => {
                if (x.Contains("www."))
                {
                    urlsSemWWW.Add(x.Replace("www.", string.Empty));
                }
            });
           
            return string.Join(", ", urls.Union(urlsSemWWW));
        }
    }
}
