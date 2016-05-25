using Carubbi.Datatables;
using Itanio.Leads.Domain.Entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.WebUI.Models
{
    public class ProjetoViewModel
    {
        [DataTablesColumn(Hidden = true, PrimaryKey = true)]
        public int? Id { get; set; }

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

        internal static ProjetoViewModel FromEntity(Projeto projeto)
        {
            return new ProjetoViewModel
            {
                Id = projeto.Id,
                Nome = projeto.Nome,
                Ativo = projeto.Ativo,
                UrlBase = projeto.UrlBase
            };
        }

        internal static Projeto ToEntity(ProjetoViewModel viewModel, ICollection<Arquivo> arquivos)
        {
            return new Projeto
            {
                Ativo = viewModel.Ativo,
                Id = viewModel.Id.HasValue? viewModel.Id.Value : 0,
                Nome = viewModel.Nome,
                UrlBase = viewModel.UrlBase,
                Arquivos = arquivos
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
