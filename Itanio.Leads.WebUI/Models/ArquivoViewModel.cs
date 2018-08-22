using System;
using System.Collections.Generic;
using Carubbi.Datatables;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.WebUI.Models
{
    public class ArquivoViewModel
    {
        [DataTablesColumn(Hidden = true, PrimaryKey = true)]
        public string Id { get; set; }

        [DataTablesColumn(Order = 1, Header = "Nome")]
        public string NomeArquivo { get; set; }

        public bool Ativo { get; set; }

        [DataTablesColumn(Order = 2, SortMap = "Ativo", Header = "Ativo")]
        public string AtivoDescr => Ativo
            ? "Sim"
            : "Não";

        [DataTablesColumn(Order = 3, Header = "URL")]
        public string Url { get; set; }

        internal static ArquivoViewModel FromEntity(Arquivo arquivo)
        {
            return new ArquivoViewModel
            {
                Id = arquivo.Id.ToString(),
                NomeArquivo = arquivo.NomeArquivo,
                Ativo = arquivo.Ativo,
                Url = arquivo.Url
            };
        }

        internal static Arquivo ToEntity(ArquivoViewModel viewModel, Projeto projeto)
        {
            return new Arquivo
            {
                Ativo = viewModel.Ativo,
                Id = !string.IsNullOrWhiteSpace(viewModel.Id) ? new Guid(viewModel.Id) : Guid.Empty,
                NomeArquivo = viewModel.NomeArquivo,
                Projeto = projeto,
                Url = viewModel.Url
            };
        }

        internal static IEnumerable<ArquivoViewModel> FromEntityCollection(IEnumerable<Arquivo> entities)
        {
            foreach (var item in entities) yield return FromEntity(item);
        }
    }
}