﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Itanio.Leads.WebUI.Models
{
    public class ModalFormViewModel
    {
        public string Id { get; set; }

        public string PartialViewName { get; set; }

        public string Title { get; set; }

        public object ViewModel { get; set; }

        public ModalSize Size { get; set; }
    }
}