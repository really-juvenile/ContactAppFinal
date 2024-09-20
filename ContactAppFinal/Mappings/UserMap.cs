using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactAppFinal.Models;
using FluentNHibernate.Mapping;

namespace ContactAppFinal.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.FName);
            Map(x => x.LName);
            Map(x => x.Password);
            Map(x => x.IsAdmin);
            Map(x => x.IsActive);

            References(x => x.Role)
                .Column("RoleId")
                .Cascade.SaveUpdate()
                .Not.Nullable();

            HasMany(x => x.Contacts)
                .Cascade.All()
                .Inverse()
                .KeyColumn("UserId");
        }
    }
}