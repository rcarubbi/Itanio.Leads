using Itanio.Leads.Domain.Entidades;
using System;

namespace Itanio.Leads.WebUI.Models
{
    public class AcessoViewModel
    {
        public string Url { get; set; }

        public string IP { get; set; }

        public string UserAgentInfo { get; set; }

        public string IdProjeto { get; set; }

        public string TipoNavegador { get; set; }

        public string Email { get; set; }

        public string IdArquivo { get; set; }

        public string Guid { get; set; }

        internal Acesso ToEntity(Visitante visitante, Arquivo arquivo, Projeto projeto)
        {
            return new Acesso
            {
                Ativo = true,
                DataHoraAcesso = DateTime.Now,
                TipoNavegador = (TipoNavegador)Enum.Parse(typeof(TipoNavegador), this.TipoNavegador),
                Url = this.Url,
                IP = this.IP,
                UserAgent = this.UserAgentInfo,
                Visitante = visitante,
                Arquivo = arquivo,
                Projeto = projeto,
                Guid = this.Guid
            };
        }
    }
}
