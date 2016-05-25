using Carubbi.Mailer.Interfaces;
using Carubbi.Utils.Data;
using Carubbi.Utils.IoC;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace Itanio.Leads.WebApi.Servicos
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
                    Guid = guid
                };

                SincronizarAcessos(visitante);
                visitanteRepo.Adicionar(visitante);
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
            message.Subject = "Gastro Educação - Recuperação de senha";
            message.IsBodyHtml = true;
            var corpo = arquivo.Projeto.TemplateEmail;
            corpo = corpo.Replace("{{link}}", Path.Combine(arquivo.Projeto.UrlBase, arquivo.Url, arquivo.NomeArquivo));
            corpo = corpo.Replace("{{Nome}}", visitante.Nome);

            message.Body = arquivo.Projeto.TemplateEmail;

            sender.Send(message);
        }

        private void SincronizarAcessos(Visitante visitante)
        {
            RepositorioAcesso acessoRepo = new RepositorioAcesso(_contexto);
            var acessosAnonimos = acessoRepo.ListarAnonimosPorGuid(visitante.Guid);
            foreach (var acesso in acessosAnonimos)
            {
                visitante.Acessos.Add(acesso);
            }
        }
    }
}
