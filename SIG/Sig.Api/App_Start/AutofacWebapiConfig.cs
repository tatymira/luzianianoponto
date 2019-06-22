using Autofac;
using Autofac.Integration.WebApi;
using Sig.Domain.IRepositories;
using Sig.Infra.__Base;
using Sig.Infra._Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace Sig.Api.App_Start
{
    public class AutofacWebapiConfig
    {
        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices());
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            builder.RegisterType<EmpresaRepository>().As<IEmpresaRepository>();
            builder.RegisterType<HorarioRepository>().As<IHorarioRepository>(); 
            builder.RegisterType<LinhaRepository>().As<ILinhaRepository>();
            builder.RegisterType<ParadaRepository>().As<IParadaRepository>();
            builder.RegisterType<TipoRepository>().As<ITipoRepository>();
            builder.RegisterType<SugestaoRepository>().As<ISugestaoRepository>();

            return builder.Build();
        }
    }
}