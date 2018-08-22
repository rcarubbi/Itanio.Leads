using System.Linq;
using Carubbi.Security;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;

namespace Itanio.Leads.Domain.Servicos
{
    public class ServicoAutenticacao
    {
        private readonly IContexto _contexto;

        private readonly SymmetricCrypt _criptografia;

        public ServicoAutenticacao(IContexto contexo)
        {
            _contexto = contexo;

            _criptografia = new SymmetricCrypt(SymmetricCryptProvider.TripleDES);
            var parametroRepo = new RepositorioParametro(_contexto);
            _criptografia.Key = parametroRepo.ObterValorPorChave(Parametro.CHAVE_CRIPTOGRAFIA);
        }

        public bool Login(string conta, string senha)
        {
            var senhaCriptografada = _criptografia.Encrypt(senha);
            return _contexto.Set<Usuario>().Any(u => u.Conta == conta && u.Senha == senhaCriptografada);
        }

        public string RecuperarSenha(string email)
        {
            var repo = new RepositorioUsuario(_contexto);
            var usuario = repo.ObterPorEmail(email);
            if (usuario == null)
                return null;
            return _criptografia.Decrypt(usuario.Senha);
        }
    }
}