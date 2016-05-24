using Carubbi.Utils.Security;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.Domain.Servicos
{
    public class ServicoAutenticacao
    {
        private IContexto _contexto;
        public ServicoAutenticacao(IContexto contexo)
        {
            _contexto = contexo;

            _criptografia = new CriptografiaSimetrica(SymmetricCryptProvider.TripleDES);
            RepositorioParametro parametroRepo = new RepositorioParametro(_contexto);
            _criptografia.Key = parametroRepo.ObterValorPorChave(Parametro.CHAVE_CRIPTOGRAFIA);
        }

        private CriptografiaSimetrica _criptografia;

        public bool Login(string conta, string senha)
        {
            var senhaCriptografada = _criptografia.Encrypt(senha);
            return _contexto.Set<Usuario>().Any(u => u.Conta == conta && u.Senha == senhaCriptografada);
        }

        public string RecuperarSenha(string email)
        {
            RepositorioUsuario repo = new RepositorioUsuario(_contexto);
            Usuario usuario = repo.ObterPorEmail(email);
            if (usuario == null)
                return null;
            else
                return _criptografia.Decrypt(usuario.Senha);

        }

    }
}
