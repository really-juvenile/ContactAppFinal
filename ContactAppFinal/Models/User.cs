using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactAppFinal.Models
{
    public class User
    {
        public virtual int Id { get; set; }
        public virtual string FName { get; set; }
        public virtual string LName { get; set; }
        public virtual string Password {get; set; }
        public virtual bool IsAdmin { get; set; }
        public virtual bool IsActive { get; set; } // For soft delete
        public virtual Role Role { get; set; } = new Role();

        public virtual IList<Contact> Contacts { get; set; } = new List<Contact>();
    
    }
}