using Carubbi.Mailer.Interfaces;
using Carubbi.Utils.Data;
using Carubbi.Utils.IoC;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using System;
using System.Linq;
using System.Net.Mail;

namespace Itanio.Leads.Api.Servicos
{
    public class ServicoVisitante
    {

        private IContexto _contexto;
        public ServicoVisitante(IContexto contexo)
        {
            _contexto = contexo;
        }

        public Visitante ObterVisitante(string email)
        {
            Visitante visitante = null;
            RepositorioVisitante visitanteRepo = new RepositorioVisitante(_contexto);
            if (!string.IsNullOrEmpty(email))
            {
                visitante = visitanteRepo.ObterPorEmail(email);
            }

            return visitante;
        }

        public Visitante CriarVisitante(string nome, string email, string guid, int idProjeto, int idArquivo)
        {
            RepositorioVisitante visitanteRepo = new RepositorioVisitante(_contexto);
            RepositorioProjeto projetoRepo = new RepositorioProjeto(_contexto);

            var projeto = projetoRepo.ObterPorId(idProjeto);
            var arquivo = projeto.Arquivos.Single(a => a.Id == idArquivo);

            var visitante = ObterVisitante(email);

            if (visitante == null)
            {
                visitante = new Visitante
                {
                    Nome = nome,
                    Email = email,
                    Ativo = true,

                };
                visitante.Identificadores.Add(new IdentificadorVisitante
                {
                    DataHora = DateTime.Now,
                    Guid = guid,
                    Ativo = true
                });
                SincronizarAcessos(visitante);
                visitanteRepo.Adicionar(visitante);
            }
            else if (!visitante.Identificadores.Any(i => i.Guid == guid))
            {
                visitante.Identificadores.Add(new IdentificadorVisitante
                {
                    Ativo = true,
                    DataHora = DateTime.Now,
                    Guid = guid
                });
                visitanteRepo.Atualizar(visitante);
            }




            NotificarAcesso(visitante, arquivo);

            return visitante;
        }

        private void NotificarAcesso(Visitante visitante, Arquivo arquivo)
        {
            RepositorioParametro parametroRepo = new RepositorioParametro(_contexto);
            var sender = ImplementationResolver.Resolve<IMailSender>();
            sender.Host = parametroRepo.ObterValorPorChave(Parametro.SMTP_SERVIDOR);
            sender.PortNumber = parametroRepo.ObterValorPorChave(Parametro.SMTP_PORTA).To(587);
            sender.UseSSL = parametroRepo.ObterValorPorChave(Parametro.SMTP_USA_SSL).To(true);
            sender.Username = parametroRepo.ObterValorPorChave(Parametro.SMTP_USUARIO);
            sender.Password = parametroRepo.ObterValorPorChave(Parametro.SMTP_SENHA);

            MailMessage message = new MailMessage();
            message.To.Add(visitante.Email);
            message.From = new MailAddress(parametroRepo.ObterValorPorChave(Parametro.REMETENTE_EMAIL));
            message.Subject = arquivo.Projeto.AssuntoEmail;
            message.IsBodyHtml = true;
            var corpo = arquivo.Projeto.TemplateEmail;
            corpo = corpo.Replace("{{nome}}", visitante.Nome)
                          .Replace("{{urlbase}}", arquivo.Projeto.UrlBase)
                          .Replace("{{email}}", visitante.Email)
                          .Replace("{{IdArquivo}}", arquivo.Id.ToString());
            message.Body = corpo;

            sender.Send(message);
        }

        private void SincronizarAcessos(Visitante visitante)
        {
            RepositorioAcesso acessoRepo = new RepositorioAcesso(_contexto);
            var acessosAnonimos = acessoRepo.ListarAnonimosPorGuid(visitante.Identificadores.Last().Guid);
            foreach (var acesso in acessosAnonimos)
            {
                visitante.Acessos.Add(acesso);
            }
        }
    }
}
