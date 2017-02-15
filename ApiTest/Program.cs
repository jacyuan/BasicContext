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
            EnableProfilerAndRunMigrations();

            var sessionFactory = IoC.Resolve<ISessionFactory>();

            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    session.CreateSQLQuery("delete from Employee;delete from Store;").ExecuteUpdate();

                    var store = new Store { Name = "Store 1" };
                    var emp = new Employee { Name = "Yuan", Age = 30, Gender = GenderEnum.Man };

                    store.AddEmployee(emp);

                    session.Save(store);

                    var suger = new Product { Name = "Suger" };
                    session.Save(suger);

                    var employees = session.Query<Employee>()
                        .ToList();

                    employees.ForEach(Console.WriteLine);
                    trans.Commit();
                }
            }

            Console.WriteLine();
            Console.ReadKey();
        }

        private static void EnableProfilerAndRunMigrations()
        {
#if DEBUG
            NHibernateProfiler.Initialize();
#endif

            var connectionString = IoC.Resolve<IDatabaseConfiguration>();

            MigratorRunner.MigrateToLatest(connectionString.ConnectionString);
        }
    }
}