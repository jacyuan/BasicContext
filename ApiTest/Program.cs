using ApiTest.Infrastructure;
using ApiTest.Infrastructure.Config;
using Database.Helper;
using Domain.Domain;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;

namespace ApiTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConfigProject();

            var sessionFactory = IoC.Resolve<ISessionFactory>();

            using (var session = sessionFactory.OpenSession())
            {
                var s = new Store { Name = "Store 1" };
                var emp = new Employee { Name = "Yuan", Age = 30, Gender = GenderEnum.Man };
                s.AddEmployee(emp);
                session.Save(s);

                var employees = session.Query<Employee>()
                    .ToList();

                employees.ForEach(Console.WriteLine);
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        private static void ConfigProject()
        {
#if DEBUG
            NHibernateProfiler.Initialize();
#endif

            var connectionString = IoC.Resolve<IDatabaseConfiguration>();

            MigratorRunner.MigrateToLatest(connectionString.ConnectionString);
        }
    }
}