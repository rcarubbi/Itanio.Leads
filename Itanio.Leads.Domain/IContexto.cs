using Carubbi.GenericRepository;

namespace Itanio.Leads.Domain
{
    public interface IContexto : IDbContext
    {
        void Salvar();

        void Atualizar<TEntidade>(TEntidade objetoAntigo, TEntidade objetoNovo) where TEntidade : class;
    }
}