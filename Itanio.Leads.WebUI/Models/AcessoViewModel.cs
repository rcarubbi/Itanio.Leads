using System;
using Itanio.Leads.Domain.Entidades;

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

        public Acesso ToEntity(Visitante visitante, Arquivo arquivo, Projeto projeto)
        {
            return new Acesso
            {
                Ativo = true,
                DataHoraAcesso = DateTime.Now,
                TipoNavegador = (TipoNavegador) Enum.Parse(typeof(TipoNavegador), TipoNavegador),
                Url = Url,
                IP = IP,
                UserAgent = UserAgentInfo,
                Visitante = visitante,
                Arquivo = arquivo,
                Projeto = projeto,
                Guid = Guid
            };
        }
    }
}