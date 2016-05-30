﻿using Itanio.Leads.DataAccess;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Itanio.Leads.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var dominios = ObterDominios();
            if (string.IsNullOrEmpty(dominios))
            {
                dominios = "*";
            }

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
            return string.Join(", ", projetoRepo.ListarAtivos().Select(x => string.Format("{0}://{1}:{2}", new Uri(x.UrlBase).Scheme, new Uri(x.UrlBase).Host, new Uri(x.UrlBase).Port)));
        }
    }
}