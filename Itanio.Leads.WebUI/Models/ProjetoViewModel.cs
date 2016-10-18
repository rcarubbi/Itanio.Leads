using Carubbi.Datatables;
using Itanio.Leads.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Itanio.Leads.WebUI.Models
{
    public class ProjetoViewModel
    {
        [DataTablesColumn(Hidden = true, PrimaryKey = true)]
        public string Id { get; set; }

        [DataTablesColumn(Order = 1)]
        public string Nome { get; set; }

        public bool Ativo { get; set; }

        [DataTablesColumn(Order = 2, SortMap = "Ativo", Header = "Ativo")]
        public string AtivoDescr
        {
            get
            {
                return Ativo
                    ? "Sim"
                    : "Não";
            }
        }

        [DataTablesColumn(Order = 3, Header = "URL base")]
        public string UrlBase { get; set; }

        [AllowHtml]
        public string TemplateEmail { get; set; }

        public string AssuntoEmail { get; set; }

        public string RemetenteEmail { get; set; }

        public string RemetenteNome { get; set; }


        public string LandPage { get; set; }
        internal static ProjetoViewModel FromEntity(Projeto projeto)
        {
            return new ProjetoViewModel
            {
                Id = projeto.Id.ToString(),
                Nome = projeto.Nome,
                Ativo = projeto.Ativo,
                UrlBase = projeto.UrlBase,
                TemplateEmail = projeto.TemplateEmail,
                AssuntoEmail = projeto.AssuntoEmail,
                RemetenteEmail = projeto.RemetenteEmail,
                RemetenteNome = projeto.RemetenteNome,
                LandPage = projeto.LandPage
            };
        }

        internal static Projeto ToEntity(ProjetoViewModel viewModel, ICollection<Arquivo> arquivos)
        {
            return new Projeto
            {
                Ativo = viewModel.Ativo,
                Id = !string.IsNullOrWhiteSpace(viewModel.Id)? new Guid(viewModel.Id) : Guid.Empty,
                Nome = viewModel.Nome,
                UrlBase = viewModel.UrlBase,
                TemplateEmail = viewModel.TemplateEmail,
                AssuntoEmail = viewModel.AssuntoEmail,
                RemetenteNome = viewModel.RemetenteNome,
                RemetenteEmail = viewModel.RemetenteEmail,
                Arquivos = arquivos,
                LandPage = viewModel.LandPage
            };
        }

        internal static IEnumerable<ProjetoViewModel> FromEntityCollection(IEnumerable<Projeto> entities)
        {
            foreach (var item in entities)
            {
                yield return FromEntity(item);
            }
        }
    }
}
