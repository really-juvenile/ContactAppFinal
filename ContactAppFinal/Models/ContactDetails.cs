using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactAppFinal.Models
{
    public class ContactDetails
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual string Value { get; set; }

        public virtual Contact Contact { get; set; } = new Contact();
    }
}