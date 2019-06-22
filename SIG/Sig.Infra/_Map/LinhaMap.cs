using FluentNHibernate.Mapping;
using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Map
{
    public class LinhaMap : ClassMap<Linha>
    {
        public LinhaMap()
        {
            Id(x => x.Id).Column("idlinha").GeneratedBy.Identity();
            Map(x => x.Nome);
            Map(x => x.Tarifa);
            Map(x => x.Origem);
            Map(x => x.Destino);
            Map(x => x.Numero);
            Map(x => x.LinhaMap);
            References(x => x.Tipo).Column("idtipo").Not.LazyLoad();
            References(x => x.Empresa).Column("idempresa").Not.LazyLoad();
            HasMany(x => x.Horarios).KeyColumn("idlinha").Inverse().Cascade.All();

            Table("linha");
        }

    }
}