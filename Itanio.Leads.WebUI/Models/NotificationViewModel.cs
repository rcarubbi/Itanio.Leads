using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.WebUI.Models
{
    public class NotificationViewModel
    {
        public string Message { get; set; }
        public string Title { get; set; }
        public NotificationType Type { get; set; }
    }

    public enum NotificationType : int
    {
        Success,
        Error,
        Warning,
        Info
    }
}
