using FluentNHibernate.Mapping;
using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Map
{
    public class TipoMap : ClassMap<Tipo>

    {
        public TipoMap()
        {
            Id(x => x.Id).Column("idtipo").GeneratedBy.Identity();
            Map(x => x.Descricao);

            Table("tipo");
        }

    }
}