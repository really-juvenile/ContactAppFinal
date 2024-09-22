using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactAppFinal.ViewModels
{
    public class ContactDetailsDTO
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public string Value { get; set; } = string.Empty;
    }
}