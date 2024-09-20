using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContactAppFinal.ViewModels
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }

        public RoleDTO Role { get; set; }
    }
}