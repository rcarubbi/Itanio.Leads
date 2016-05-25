namespace Itanio.Leads.DataAccess.Migrations
{
    using Carubbi.Utils.Security;
    using Domain.Entidades;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Itanio.Leads.DataAccess.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Itanio.Leads.DataAccess.Contexto context)
        {
            var criptografia = new CriptografiaSimetrica(SymmetricCryptProvider.TripleDES);
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
               Valor = "raphael@itanio.com.br",
               Ativo = true
           },
            new Parametro
            {
                Chave = Parametro.SMTP_PORTA,
                Valor = "587",
                Ativo = true
            },
             new Parametro
             {
                 Chave = Parametro.SMTP_SENHA,
                 Valor = "raphakf0612",
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
                   Valor = "true",
                   Ativo = true
               },
               new Parametro
               {
                   Chave = Parametro.SMTP_USUARIO,
                   Valor = "raphael@itanio.com.br",
                   Ativo = true
               });
            context.Salvar();
            #endregion
        }
    }
}
