using Database.Helper;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using System;
using System.Configuration;
using System.Linq;
using Domain.Domain;
using NHibernate.Linq;

namespace ApiTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConfigProject();

            var sessionFactory = SessionHelper.GestSessionFactory();

            using (var session = sessionFactory.OpenSession())
            {
                var s = new Store { Name = "Store 1" };
                var emp = new Employee { Store = s, Name = "Yuan", Age = 30, Gender = GenderEnum.Man };

                session.Save(s);
                session.Save(emp);

                var employees = session.Query<Employee>()
                    .ToList();


            }

            Console.WriteLine();
            Console.ReadKey();
        }

        private static void ConfigProject()
        {
#if DEBUG
            NHibernateProfiler.Initialize();
#endif
            var connectionString = ConfigurationManager.ConnectionStrings["DESKTOP-FV5SJ2S"].ConnectionString;

            MigratorRunner.MigrateToLatest(connectionString);
        }
    }
}