using FluentNHibernate.Mapping;
using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Map
{
    public class ParadaMap : ClassMap<Parada>
    {
        public ParadaMap()
        {

            Id(x => x.Id).Column("idparada").GeneratedBy.Identity();
            Map(x => x.Numero);
            Map(x => x.Latitude);
            Map(x => x.Longitude);

            Table("parada");
        }
    }
}