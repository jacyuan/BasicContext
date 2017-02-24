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

        private static string _cleanBase = @"
delete from Employee;
delete from Supermarket;
delete from GroceryStore;
delete from Store;
delete from Product;
delete from Store_product;";

        private static void Main(string[] args)
        {
            EnableProfilerAndRunMigrations();

            var sessionFactory = IoC.Resolve<ISessionFactory>();

            using (var session = sessionFactory.OpenSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    //delete all existing data in base
                    session.CreateSQLQuery(_cleanBase).ExecuteUpdate();

                    var address = new Address
                    {
                        Country = "France",
                        Street = "Av St-Priest",
                        StreetNumber = 10,
                        ZipCode = "34000"
                    };

                    //                    var store = new Store { Name = "Store 1", Address = address };

                    //                    var emp1 = new StoreOwner { Name = "Yuan", Age = 30, Gender = GenderEnum.Man };
                    //                    store.AddEmployee(emp1);

                    var emp2 = new Employee { Name = "test", Age = 30, Gender = GenderEnum.Woman };
                    session.Save(emp2);

                    var emp3 = new StockHolder { Name = "test1", Age = 40, Gender = GenderEnum.Woman };
                    session.Save(emp3);

                    var suger = new Product { Name = "Suger" };
                    session.Save(suger);

                    var flavor = new Product { Name = "Flavor" };
                    session.Save(flavor);
                    //
                    //                    store.AddProduct(suger);
                    //                    store.AddProduct(flavor);
                    //
                    //                    session.Save(store);

                    var emp = session.Query<Employee>().ToList();

                    emp.ForEach(x => Console.WriteLine(x.ToString()));

                    var supermarket1 = new Supermarket { Name = "Carrefour", Surface = 100, Address = new Address { StreetNumber = 100, Country = "France", Street = "St-Clement-De-Riviere", ZipCode = "34000" } };
                    session.Save(supermarket1);

                    var groceryStore1 = new GroceryStore { Name = "StPriestEpicerie", Address = new Address { StreetNumber = 25, Country = "France", Street = "St-Priest", ZipCode = "34090" }, IsFullDayStore = true };
                    session.Save(groceryStore1);

                    var stores = session.Query<Store>().ToList();

                    stores.ForEach(x => Console.WriteLine(x.ToString()));

                    //                    var store = session.Query<Store>().FirstOrDefault(x => x.Name == "Store 1");
                    //
                    //                    if (store != null)
                    //                    {
                    //                        store.RemoveEmployee(store.Employees.FirstOrDefault());
                    //                        store.RemoveProduct(store.Products.First());
                    //                        store.Address.StreetNumber = 20;
                    //                    }

                    trans.Commit();
                }
            }

            Console.WriteLine("finished ...");
            //Console.ReadLine();
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