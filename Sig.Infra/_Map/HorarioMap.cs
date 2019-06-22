using FluentNHibernate.Mapping;
using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Map
{
    public class HorarioMap : ClassMap<Horario>

    {
        public HorarioMap()
        {
            Id(x => x.Id).Column("idhorario").GeneratedBy.Identity();
            Map(x => x.HoraSaida);
            References(x => x.Linha).Column("idlinha").Not.LazyLoad();

            Table("horario");
        }

    }
}