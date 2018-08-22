using System;
using System.Linq;
using System.Net.Mail;
using Carubbi.Extensions;
using Carubbi.Mailer.Implementation;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;

namespace Itanio.Leads.WebUI.Servicos
{
    public class ServicoVisitante
    {
        private readonly IContexto _contexto;

        public ServicoVisitante(IContexto contexo)
        {
            _contexto = contexo;
        }

        public Visitante ObterVisitante(Guid guid)
        {
            Visitante visitante = null;
            var visitanteRepo = new RepositorioVisitante(_contexto);
            visitante = visitanteRepo.ObterPorIdentificador(guid.ToString());
            return visitante;
        }


        public Visitante ObterVisitante(string email)
        {
            Visitante visitante = null;
            var visitanteRepo = new RepositorioVisitante(_contexto);
            if (!string.IsNullOrEmpty(email)) visitante = visitanteRepo.ObterPorEmail(email);

            return visitante;
        }


        public Visitante CriarVisitante(string nome, string email, string guid, Guid idProjeto, Guid idArquivo)
        {
            var visitanteRepo = new RepositorioVisitante(_contexto);
            var projetoRepo = new RepositorioProjeto(_contexto);

            var projeto = projetoRepo.ObterPorId(idProjeto);
            var arquivo = projeto.Arquivos.Single(a => a.Id == idArquivo);

            var visitante = ObterVisitante(email);

            if (visitante == null)
                visitante = new Visitante
                {
                    Nome = nome,
                    Email = email,
                    Ativo = true
                };

            var guidIdentificador = string.Empty;
            if (ObterVisitante(new Guid(guid)) == null)
                guidIdentificador = guid;
            else
                guidIdentificador = Guid.NewGuid().ToString();

            if (!visitante.Identificadores.Any(i => i.Guid == guidIdentificador))
                visitante.Identificadores.Add(new IdentificadorVisitante
                {
                    DataHora = DateTime.Now,
                    Guid = guidIdentificador,
                    Ativo = true
                });

            SincronizarAcessos(visitante, guid);
            if (visitante.Id != Guid.Empty)
                visitanteRepo.Atualizar(visitante);
            else
                visitanteRepo.Adicionar(visitante);

            NotificarAcesso(visitante, arquivo);

            return visitante;
        }

        private void NotificarAcesso(Visitante visitante, Arquivo arquivo)
        {
            var parametroRepo = new RepositorioParametro(_contexto);
            var sender = new SmtpSender();
            sender.Host = parametroRepo.ObterValorPorChave(Parametro.SMTP_SERVIDOR);
            sender.PortNumber = parametroRepo.ObterValorPorChave(Parametro.SMTP_PORTA).To(587);
            sender.UseSsl = parametroRepo.ObterValorPorChave(Parametro.SMTP_USA_SSL).To(true);
            sender.Username = parametroRepo.ObterValorPorChave(Parametro.SMTP_USUARIO);
            sender.Password = parametroRepo.ObterValorPorChave(Parametro.SMTP_SENHA);
            sender.UseDefaultCredentials =
                parametroRepo.ObterValorPorChave(Parametro.SMTP_USAR_CREDENCIAIS_PADRAO).To(true);

            var message = new MailMessage();
            message.To.Add(visitante.Email);
            message.From = new MailAddress(arquivo.Projeto.RemetenteEmail, arquivo.Projeto.RemetenteNome);
            message.Subject = arquivo.Projeto.AssuntoEmail;
            message.IsBodyHtml = true;
            var corpo = arquivo.Projeto.TemplateEmail;
            corpo = corpo.Replace("{{nome}}", visitante.Nome)
                .Replace("{{primeiro_nome}}", visitante.Nome.Split(' ')[0])
                .Replace("{{descricao_arquivo}}", arquivo.Descricao)
                .Replace("{{urlbase}}", arquivo.Projeto.UrlBase)
                .Replace("{{email}}", visitante.Email)
                .Replace("{{IdArquivo}}", arquivo.Id.ToString());
            message.Body = corpo;

            sender.Send(message);
        }

        private void SincronizarAcessos(Visitante visitante, string guid)
        {
            var acessoRepo = new RepositorioAcesso(_contexto);
            var acessosAnonimos = acessoRepo.ListarAnonimosPorGuid(guid);
            foreach (var acesso in acessosAnonimos)
            {
                acesso.Guid = visitante.Identificadores.Last().Guid;
                visitante.Acessos.Add(acesso);
            }
        }
    }
}