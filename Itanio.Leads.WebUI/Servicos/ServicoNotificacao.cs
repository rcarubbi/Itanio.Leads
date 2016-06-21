using Carubbi.Mailer.Implementation;
using Carubbi.Mailer.Interfaces;
using Carubbi.Utils.Data;
using Carubbi.Utils.IoC;
using Itanio.Leads.Domain;
using Itanio.Leads.Domain.Entidades;
using Itanio.Leads.Domain.Repositorios;
using System.Net.Mail;
using System.Web;

namespace Itanio.Leads.WebUI.Servicos
{
    internal class ServicoNotificacao
    {
        private string _baseUrl;
        private string _baseTemplatePath;
        private IContexto _contexto;
        public ServicoNotificacao(IContexto contexto)
        {
            _contexto = contexto;
            _baseTemplatePath = HttpContext.Current.Server.MapPath("~\\EmailTemplates");
            _baseUrl = $"{HttpContext.Current.Request.Url.Scheme}://{HttpContext.Current.Request.Url.Authority}";
        }

        internal void NotificarRecuperacaoSenha(string email, string senhaRecuperada, string link)
        {
            RepositorioUsuario usuarioRepo = new RepositorioUsuario(_contexto);
            Usuario usuario = usuarioRepo.ObterPorEmail(email);

            RepositorioParametro parametroRepo = new RepositorioParametro(_contexto);

            string corpo = $@"<!DOCTYPE html>
                            <html>
                            <head>
                                <meta name='viewport' content='width=device-width' />
                                <title>

                                </title>
                            </head>
                            <body>
                                <div>
                                    Olá {usuario.Nome},
                                </div>
                                <div>
                                    Sua senha é {senhaRecuperada}.
                                </div>
                                <div>
                                    Clique <a href='{_baseUrl + link}'>aqui</a> para acessar o sistema.
                                </div>
                            </body>
                            </html>";

          
            MailMessage message = new MailMessage();
            message.To.Add(email);
            message.From = new MailAddress(parametroRepo.ObterValorPorChave(Parametro.REMETENTE_EMAIL), parametroRepo.ObterValorPorChave(Parametro.REMETENTE_EMAIL_NOME));
            message.Subject = "Gastro Educação - Recuperação de senha";
            message.IsBodyHtml = true;
            message.Body = corpo;


            var sender = new SmtpSender();
            sender.Host = parametroRepo.ObterValorPorChave(Parametro.SMTP_SERVIDOR);
            sender.PortNumber = parametroRepo.ObterValorPorChave(Parametro.SMTP_PORTA).To(587);
            sender.UseSSL = parametroRepo.ObterValorPorChave(Parametro.SMTP_USA_SSL).To(true);
            sender.Username = parametroRepo.ObterValorPorChave(Parametro.SMTP_USUARIO);
            sender.Password = parametroRepo.ObterValorPorChave(Parametro.SMTP_SENHA);
            sender.UseDefaultCredentials = parametroRepo.ObterValorPorChave(Parametro.SMTP_USAR_CREDENCIAIS_PADRAO).To(true);

            sender.Send(message);
        }
    }
}