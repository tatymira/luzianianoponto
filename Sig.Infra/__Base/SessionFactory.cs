using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sig.Infra.__Base
{
    public class SessionFactory
    {
        public static ISessionFactory GetFactory()
        {
            var configuration = Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(x => x.FromConnectionStringWithKey("DataBase"))
                .ShowSql()
                .FormatSql())
                .CurrentSessionContext("web")
                .Mappings(c => c.FluentMappings.AddFromAssemblyOf<_Map.LinhaMap>());

            return configuration.BuildSessionFactory();
            
        }

        //public static string Get()
        //{
        //    var uriString = ConfigurationManager.AppSettings["ElephantSQL"];
        //    var uri = new Uri(uriString);
        //    var db = uri.AbsolutePath.Trim('/');
        //    var user = uri.UserInfo.Split(':')[0];
        //    var passwd = uri.UserInfo.Split(':')[1];
        //    var port = uri.Port > 0 ? uri.Port : 5432;
        //    var connStr = string.Format("Server={0};Database={1};User Id={2};Password={3};Port={4}",
        //        uri.Host, db, user, passwd, port);
        //    return connStr;
        //}
    }
}