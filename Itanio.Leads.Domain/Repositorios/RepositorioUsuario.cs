using System.Linq;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.Domain.Repositorios
{
    public class RepositorioUsuario
    {
        private readonly IContexto _contexto;

        public RepositorioUsuario(IContexto contexto)
        {
            _contexto = contexto;
        }

        public Usuario ObterPorEmail(string email)
        {
            return _contexto.Set<Usuario>().FirstOrDefault(u => u.Email == email);
        }

        public Usuario ObterPorConta(string conta)
        {
            return _contexto.Set<Usuario>().Single(u => u.Conta == conta);
        }
    }
}