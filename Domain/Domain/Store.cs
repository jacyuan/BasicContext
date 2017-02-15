using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections.Generic;
using System.Linq;
using FluentNHibernate.Utils;
using NHibernate.Util;

namespace Domain.Domain
{
    public class Store : Entity
    {
        public virtual string Name { get; set; }
        public virtual IEnumerable<Employee> Employees { get; set; }
        public virtual IEnumerable<Product> Products { get; set; }
        public virtual Address Address { get; set; }

        public virtual void AddEmployee(Employee emp)
        {
            if (emp == null)
                return;

            if (Employees == null)
                Employees = new Employee[] { };

            if (Employees.Contains(emp))
                return;

            emp.Store = this;
            Employees = Employees.Concat(new[] { emp });
        }

        public virtual void AddEmployees(IEnumerable<Employee> emps)
        {
            var employees = emps as Employee[] ?? emps.ToArray();
            if (emps == null || !employees.Any()) return;

            employees.ForEach(AddEmployee);
        }

        public virtual void RemoveEmployee(Employee emp)
        {
            if (emp == null || Employees == null || !Employees.Any() || !Employees.Contains(emp))
                return;

            Employees = Employees.Where(x => x.Id != emp.Id).ToArray();
            emp.Store = null;
        }

        public virtual void AddProduct(Product prod)
        {
            if (prod == null)
                return;

            if (Products == null)
                Products = new Product[] { };

            if (Products.Contains(prod))
                return;

            Products = Products.Concat(new[] { prod });
        }

        public virtual void AddProducts(IEnumerable<Product> prods)
        {
            var products = prods as Product[] ?? prods.ToArray();
            if (prods == null || !products.Any()) return;

            products.ForEach(AddProduct);
        }

        public virtual void RemoveProduct(Product prod)
        {
            if (prod == null || Products == null || !Products.Any() || !Products.Contains(prod))
                return;

            Products = Products.Except(prod);
        }

        public override string ToString()
        {
            return $"{Name}";
        }
    }

    public class StoreMap : ClassMapping<Store>
    {
        public StoreMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));

            //not necessary
            //Property(x => x.Name);

            Bag(x => x.Employees, cm =>
              {
                  //                  cm.Access(Accessor.Field);
                  cm.Key(km =>
                  {
                      km.Column("Store_id");
                      km.NotNullable(false);
                  });
                  cm.Cascade(Cascade.All);
                  cm.Inverse(true);
              }, m => m.OneToMany());

            Bag(x => x.Products, cm =>
              {
                  cm.Table("Store_product");
                  cm.Key(km =>
                  {
                      km.Column("Store_id");
                      km.NotNullable(true);
                  });
                  cm.Cascade(Cascade.All);
              },
              m => m.ManyToMany(mm =>
              {
                  mm.Column("Product_id");
              }));

            
            Component(x => x.Address, map =>
            {
                //not necessary to go further for inner mapping of Address
                map.Property(m => m.Country);
                map.Property(m => m.StreetNumber);
                map.Property(m => m.Street);
                map.Property(m => m.ZipCode);
            });

            DynamicUpdate(true);
        }
    }
}