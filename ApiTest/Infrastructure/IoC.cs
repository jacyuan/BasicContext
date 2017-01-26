using ApiTest.Infrastructure.Config;
using ApiTest.Infrastructure.Datebase;
using Autofac;

namespace ApiTest.Infrastructure
{
    public class IoC
    {
        internal static IContainer Container { get; }

        static IoC()
        {
            var builder = new ContainerBuilder();

            var config = new AppConfiguration();

            builder.RegisterInstance(config)
                .As<IDatabaseConfiguration>()
                .SingleInstance();

            builder.RegisterModule(new NHibernateModule { Configuration = config });

            Container = builder.Build();
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}