using FluentNHibernate.Mapping;
using Sig.Domain.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sig.Infra._Map
{
    public class SugestaoMap : ClassMap<Sugestao>
    {
        public SugestaoMap()
        {

            Id(x => x.Id).Column("idsugestao").GeneratedBy.Identity();
            Map(x => x.Longitude);
            Map(x => x.Latitude);
            Map(x => x.Descricao);
            Map(x => x.Alocacao);
            Map(x => x.Ativo);
            Map(x => x.Data);
            References(x => x.Linha).Column("idlinha");

            Table("sugestao");
        }
    }
}