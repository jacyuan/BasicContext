using System.Configuration;
using System.Data;
using Domain.Domain;
using FluentNHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using Configuration = NHibernate.Cfg.Configuration;

namespace ApiTest
{
    public static class SessionHelper
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory GestSessionFactory()
        {
            return _sessionFactory ?? (_sessionFactory = BuildSessionFactory());
        }

        private static ISessionFactory BuildSessionFactory()
        {
            var config = new Configuration().DataBaseIntegration(db =>
             {
                 db.Dialect<MsSql2012Dialect>();
                 db.IsolationLevel = IsolationLevel.ReadCommitted;
                 db.BatchSize = 50;
                 db.ConnectionString = ConfigurationManager.ConnectionStrings["DESKTOP-FV5SJ2S"].ConnectionString;
             });

            config.AddMappingsFromAssembly(typeof(Store).Assembly);

            return config.BuildSessionFactory();
        }
    }
}