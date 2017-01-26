using ApiTest.Infrastructure.Config;
using Autofac;
using Domain.Domain;
using FluentNHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using System.Data;

namespace ApiTest.Infrastructure.Datebase
{
    public class NHibernateModule : Module
    {
        public AppConfiguration Configuration { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(BuildSessionFactory())
                .As<ISessionFactory>()
                .SingleInstance();

            base.Load(builder);
        }

        private ISessionFactory BuildSessionFactory()
        {
            var config = new Configuration().DataBaseIntegration(db =>
            {
                db.Dialect<MsSql2012Dialect>();
                db.IsolationLevel = IsolationLevel.ReadCommitted;
                db.BatchSize = 50;
                db.ConnectionString = Configuration.ConnectionString;
            });

            config.AddMappingsFromAssembly(typeof(Store).Assembly);

            return config.BuildSessionFactory();
        }
    }
}