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
using NHibernate.Util;

namespace ApiTest.Infrastructure.Datebase
{
    public class NHibernateModule : Module
    {
        public AppConfiguration Configuration { get; set; }

        private static readonly Func<Type, bool> IsRootEntityCondition = t => t.BaseType == typeof(Entity);
        private static readonly Func<Type, bool> IsEntityCondition = t => typeof(Entity).IsAssignableFrom(t);

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
            var allEntityMapTypes = typeof(Employee).Assembly.GetExportedTypes();

            //build a dico with 
            // key      : original entity type
            // value    : mapping class
            var entityTypeMapDico = allEntityMapTypes
                //for all mapping classes, having a generic type defined for mapping, such as : ClassMapping<xxx>
                .Where(x => 
                    typeof(IEntityAttributesMapper).IsAssignableFrom(x) 
                    && x.BaseType != null 
                    && x.BaseType.GenericTypeArguments.Any()
                )
                .ToDictionary(x => x.BaseType.GenericTypeArguments[0], x => x);

            var mapper = new ConventionModelMapper();
            mapper.IsRootEntity((type, declared) => IsRootEntityCondition(type));
            mapper.IsEntity((type, declared) => IsEntityCondition(type));

            //add mapping classes seperately by respecting inheritance orders : root classes first, then sub classes
            AddClassMappingsByInheritanceOrder(mapper, entityTypeMapDico, null, IsRootEntityCondition);

            var mappings = mapper.CompileMappingForEachExplicitlyAddedEntity();

            mappings.ForEach(config.AddMapping);
        }

        private static void AddClassMappingsByInheritanceOrder(ConventionModelMapper mapper, IDictionary<Type, Type> entityClassesAndMappingsToAdd, Type[] justAddedParentClasses = null, Func<Type, bool> isRootEntity = null)
        {
            #region arg exception

            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));

            //if nothing has been added to mapper yet, we then need to have isRootEntity function to determinate root classes
            //in this case, isRootEntity should be not be null
            if ((justAddedParentClasses == null || !justAddedParentClasses.Any())
                && isRootEntity == null)
            {
                throw new ArgumentNullException(nameof(isRootEntity));
            }

            #endregion

            //stop condition : if nothing left to add, quit
            if (entityClassesAndMappingsToAdd == null || !entityClassesAndMappingsToAdd.Any())
                return;

            var shouldBeAdded = isRootEntity;

            if (justAddedParentClasses != null && justAddedParentClasses.Any())
            {
                shouldBeAdded = x => justAddedParentClasses.Contains(x.BaseType);
            }

            var entitesToAdd = entityClassesAndMappingsToAdd.Keys.Where(shouldBeAdded).ToArray();

            mapper.AddMappings(entityClassesAndMappingsToAdd.Where(x => entitesToAdd.Contains(x.Key)).Select(x => x.Value));

            //recursive
            AddClassMappingsByInheritanceOrder(
                mapper,
                entityClassesAndMappingsToAdd.Where(kv => !entitesToAdd.Contains(kv.Key)).ToDictionary(x => x.Key, x => x.Value),
                entitesToAdd
           );
        }
    }
}