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
//            var connectionString = ConfigurationManager.ConnectionStrings["DESKTOP-FV5SJ2S"].ConnectionString;
            var connectionString = ConfigurationManager.ConnectionStrings["PORT-DOTN-21"].ConnectionString;

            MigratorRunner.MigrateToLatest(connectionString);
        }
    }
}