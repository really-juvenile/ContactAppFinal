using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ContactAppFinal.Models;
using FluentNHibernate.Mapping;

namespace ContactAppFinal.Mappings
{
    public class ContactDetailsMap : ClassMap<ContactDetails>
    {
        public ContactDetailsMap()
        {
            Table("ContactDetails");
            Id(x => x.Id);

            Map(x => x.Type);
            Map(x => x.Value);

            References(x => x.Contact)
                .Column("ContactId")
                .Not.Nullable();
        }
    }
}