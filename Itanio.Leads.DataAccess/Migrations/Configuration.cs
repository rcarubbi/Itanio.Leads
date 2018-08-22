using System.Data.Entity.Migrations;
using Carubbi.Security;
using Itanio.Leads.Domain.Entidades;

namespace Itanio.Leads.DataAccess.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexto context)
        {
            var criptografia = new SymmetricCrypt(SymmetricCryptProvider.TripleDES);
            criptografia.Key = "IT_Newest_49387_In";

            #region Usuario

            context.Set<Usuario>().AddOrUpdate(u => u.Conta,
                new Usuario
                {
                    Nome = "Agente Interno",
                    Ativo = true,
                    Conta = "AgenteInterno",
                    Email = "AgenteInterno@sistema.com.br",
                    Senha = criptografia.Encrypt("AgenteInterno")
                });
            context.Salvar();

            #endregion

            #region Parametro

            context.Set<Parametro>().AddOrUpdate(p => p.Chave,
                new Parametro
                {
                    Chave = Parametro.CHAVE_CRIPTOGRAFIA,
                    Valor = "IT_Newest_49387_In",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.REMETENTE_EMAIL,
                    Valor = "leads@itanio.com.br",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.REMETENTE_EMAIL_NOME,
                    Valor = "Leads",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.SMTP_PORTA,
                    Valor = "25",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.SMTP_SENHA,
                    Valor = "raphakf061208",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.SMTP_SERVIDOR,
                    Valor = "mail.exchange.locaweb.com.br",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.SMTP_USA_SSL,
                    Valor = "false",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.SMTP_USUARIO,
                    Valor = "raphael@itanio.com.br",
                    Ativo = true
                },
                new Parametro
                {
                    Chave = Parametro.SMTP_USAR_CREDENCIAIS_PADRAO,
                    Valor = "true",
                    Ativo = true
                });
            context.Salvar();

            #endregion
        }
    }
}