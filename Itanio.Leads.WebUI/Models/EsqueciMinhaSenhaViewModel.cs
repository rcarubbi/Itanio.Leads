using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.WebUI.Models
{
    public class EsqueciMinhaSenhaViewModel
    {
        [Required]
        public string Email { get; set; }
    }
}
