using Carubbi.Datatables;
using Itanio.Leads.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.WebUI.Models
{
    public class ArquivoViewModel
    {
        [DataTablesColumn(Hidden = true, PrimaryKey = true)]
        public int? Id { get; set; }

        [DataTablesColumn(Order = 1, Header = "Nome")]
        public string NomeArquivo { get; set; }

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

        [DataTablesColumn(Order = 3, Header = "URL")]
        public string Url  { get; set; }

        internal static ArquivoViewModel FromEntity(Arquivo arquivo)
        {
            return new ArquivoViewModel
            {
                Id = arquivo.Id,
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
                Id = viewModel.Id.HasValue ? viewModel.Id.Value : 0,
                NomeArquivo = viewModel.NomeArquivo,
                Projeto = projeto,
                Url = viewModel.Url
            };
        }

        internal static IEnumerable<ArquivoViewModel> FromEntityCollection(IEnumerable<Arquivo> entities)
        {
            foreach (var item in entities)
            {
                yield return FromEntity(item);
            }
        }
    }
}
