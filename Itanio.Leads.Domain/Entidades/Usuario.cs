namespace Itanio.Leads.Domain.Entidades
{
    public class Usuario : Entidade
    {
        public string Nome { get; set; }

        public string Senha { get; set; }

        public string Conta { get; set; }

        public string Email { get; set; }
    }
}