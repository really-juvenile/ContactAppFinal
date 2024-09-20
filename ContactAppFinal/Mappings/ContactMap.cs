using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactAppFinal.Models;
using FluentNHibernate.Mapping;

namespace ContactAppFinal.Mappings
{
    public class ContactMap : ClassMap<Contact>
    {
        public ContactMap()
        {
            Table("Contacts");
            Id(x => x.Id).GeneratedBy.Identity();

            Map(x => x.FName);
            Map(x => x.LName);
            Map(x => x.IsActive);

            References(x => x.User)
                .Column("UserId")
                .Cascade.None().Nullable();

            HasMany(x => x.ContactDetails)
                .Cascade.All()
                .Inverse();
        }
    }
}