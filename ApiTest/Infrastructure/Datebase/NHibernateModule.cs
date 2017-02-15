using ApiTest.Helpers;
using ApiTest.Infrastructure.Config;
using Autofac;
using Domain.Domain;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using System.Data;
using System.Linq;

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
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            });

            AddMappings(config);

            return config.BuildSessionFactory();
        }

        private static void AddMappings(Configuration config)
        {
            var mapper = new ConventionModelMapper();

            //for all types defined inside the domain assembly
            var customTypes = typeof(Employee).Assembly.GetTypes()
                //get only those mapping classes
                .Where(x => x.IsAssignableToGenericType(typeof(NHibernate.Mapping.ByCode.Conformist.ClassMapping<>)))
                .ToArray();

            mapper.AddMappings(customTypes);

            var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();

            config.AddMapping(mappings);
        }
    }
}