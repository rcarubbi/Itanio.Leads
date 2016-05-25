using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.WebApi.Models
{
    public class VisitanteViewModel
    {
        public string Email { get; set; }

        public string Nome { get; set; }

        public string Guid { get; set; }

        public int IdProjeto { get; set; }

        public int IdArquivo { get; set; }
    }
}
