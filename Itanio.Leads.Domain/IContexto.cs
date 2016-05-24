using System.Data.Entity;

namespace Itanio.Leads.Domain
{
    public interface IContexto : Carubbi.GenericRepository.IDbContext
    {
        void Salvar();

        void Atualizar<TEntidade>(TEntidade objetoAntigo, TEntidade objetoNovo) where TEntidade : class;

       
    }
}
