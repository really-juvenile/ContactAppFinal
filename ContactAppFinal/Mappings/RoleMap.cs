using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactAppFinal.Models;
using FluentNHibernate.Mapping;

namespace ContactAppFinal.Mappings
{
    public class RoleMap : ClassMap<Role>
    {
        public RoleMap()
        {
            Table("Roles");
            Id(x => x.Id);
            
            Map(x => x.Name);
            References(r => r.User).Column("UserId").Cascade.SaveUpdate();
            
              
        }
    }
}