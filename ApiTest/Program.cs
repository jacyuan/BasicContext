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
                    session.CreateSQLQuery("delete from Employee;delete from Store;delete from Product;delete from Store_product;").ExecuteUpdate();

                    var address = new Address
                    {
                        Country = "France",
                        Street = "Av St-Priest",
                        StreetNumber = 10,
                        ZipCode = "34000"
                    };
                    var store = new Store { Name = "Store 1", Address = address };

                    var emp = new Employee { Name = "Yuan", Age = 30, Gender = GenderEnum.Man };
                    store.AddEmployee(emp);

                    var suger = new Product { Name = "Suger" };
                    session.Save(suger);

                    var flavor = new Product { Name = "Flavor" };
                    session.Save(flavor);

                    store.AddProduct(suger);
                    store.AddProduct(flavor);

                    session.Save(store);

                    store = session.Query<Store>()
                        .Where(x => x.Name == "Store 1")
                        .FirstOrDefault();

                    store.RemoveEmployee(store.Employees.FirstOrDefault());
                    store.RemoveProduct(store.Products.First());

                    trans.Commit();
                }
            }

            Console.WriteLine("finished ...");
            Console.ReadLine();
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