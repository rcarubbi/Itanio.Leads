using System.ComponentModel.DataAnnotations;

namespace Itanio.Leads.WebUI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Conta { get; set; }

        [Required]
        [UIHint("Senha")]
        public string Senha { get; set; }

        [Display(Name = "Mantenha-me conectado")]
        public bool MantenhaMeConectado { get; set; }
    }
}
