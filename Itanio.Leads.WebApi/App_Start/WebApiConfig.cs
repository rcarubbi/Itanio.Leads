using Itanio.Leads.DataAccess;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Itanio.Leads.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var projetos = ObterProjetos();
            if (string.IsNullOrEmpty(projetos))
            {
                config.EnableCors();
            }
            else
            {
                // Web API configuration and services
                var cors = new EnableCorsAttribute(projetos, "*", "*");
                config.EnableCors(cors);
            }
            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }

        private static string ObterProjetos()
        {
            IContexto contexto = new Contexto();
            RepositorioProjeto projetoRepo = new RepositorioProjeto(contexto);
            return string.Join(", ", projetoRepo.ListarAtivos().Select(x => x.UrlBase));
        }
    }
}
