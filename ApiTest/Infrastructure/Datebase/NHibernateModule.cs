using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Util;

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

            Func<Type, bool> isRootEntityCondition = t => t.BaseType == typeof(Entity);
            Func<Type, bool> isEntityCondition = t => !isRootEntityCondition(t) && typeof(Entity).IsAssignableFrom(t);

            mapper.IsRootEntity((type, declared) => isRootEntityCondition(type));
            mapper.IsEntity((type, declared) => isEntityCondition(type));
            mapper.IsTablePerClassHierarchy((type, b) => type == typeof(Employee));

            ////for all types defined inside the domain assembly
            //var customTypes = typeof(Employee).Assembly.GetTypes()
            //    //get only those mapping classes
            //    .Where(x => x.IsAssignableToGenericType(typeof(ClassMapping<>)) || x.IsAssignableToGenericType(typeof(SubclassMapping<>)))
            //    .ToArray();

            //mapper.AddMappings(customTypes);

            //var mappings = mapper.CompileMappingForAllExplicitlyAddedEntities();

            //config.AddMapping(mappings);

            var allEntityMapTypes = typeof(Employee).Assembly.GetExportedTypes();

            //            var entityTypeMapDico = allEntityMapTypes
            //                .Where(x => typeof(IEntityAttributesMapper).IsAssignableFrom(x))
            //                .ToDictionary(x => x.BaseType.GenericTypeArguments[0], x => x);
            //
            //            entityTypeMapDico.ForEach(typeMapping =>
            //            {
            //                var type = typeMapping.Key;
            //                var mapping = typeMapping.Value;
            //
            //            });

            mapper.AddMappings(allEntityMapTypes);

            //            if (allEntityTypes == null)
            //                throw new ArgumentNullException("types");
            //            HashSet<Type> typeToMap = new HashSet<Type>(allEntityTypes);
            //            foreach (Type type in this.RootClasses((IEnumerable<Type>)typeToMap))
            //            {
            //                HbmMapping mapping = this.NewHbmMapping(type.Assembly.GetName().Name, type.Namespace);
            //                this.MapRootClass(type, mapping);
            //                yield return mapping;
            //            }
            //            foreach (Type type in this.Subclasses((IEnumerable<Type>)typeToMap))
            //            {
            //                HbmMapping mapping = this.NewHbmMapping(type.Assembly.GetName().Name, type.Namespace);
            //                this.AddSubclassMapping(mapping, type);
            //                yield return mapping;
            //            }


            var mappings = mapper.CompileMappingForEachExplicitlyAddedEntity();

            foreach (var mapping in mappings)
            {
                config.AddMapping(mapping);
            }
        }
    }
}