using System.ComponentModel.DataAnnotations;

namespace Itanio.Leads.WebUI.Models
{
    public class EsqueciMinhaSenhaViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
