namespace Itanio.Leads.Domain.Entidades
{
    public class Parametro : Entidade
    {
        public const string CHAVE_CRIPTOGRAFIA = "ChaveCriptografia";
        public const string REMETENTE_EMAIL = "RemetenteEmail";
        public const string SMTP_SERVIDOR = "ServidorEmail";
        public const string SMTP_PORTA = "PortaSMTP";
        public const string SMTP_USA_SSL = "SMTPUsaSSL";
        public const string SMTP_USUARIO = "SMTPUsuario";
        public const string SMTP_SENHA = "SMTPSenha";

        public string Chave { get; set; }

        public string Valor { get; set; }
    }
}
