using System.Linq;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.WebUI.Models
{
    public class VisitanteViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }

        public bool Ativo { get; set; }

        public string Nome { get; set; }

        public string Guid { get; set; }

        public string IdProjeto { get; set; }

        public string IdArquivo { get; set; }

        internal static VisitanteViewModel FromEntity(Visitante visitante)
        {
            return new VisitanteViewModel
            {
                Id = visitante.Id.ToString(),
                Nome = visitante.Nome,
                Ativo = visitante.Ativo,
                Email = visitante.Email,
                Guid = visitante.Identificadores.Last().Guid
            };
        }
    }
}