using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Util;

namespace Domain.Domain
{
    public class Store : Entity
    {
        public virtual string Name { get; set; }
//        public virtual IEnumerable<Employee> Employees { get; set; }
//
//        public virtual void AddEmployee(Employee emp)
//        {
//            if (emp == null)
//                return;
//
//            if (Employees == null)
//                Employees = new Employee[] { };
//
//            if (Employees.Contains(emp))
//                return;
//
//            //            emp.Store = this;
//            Employees = Employees.Concat(new[] { emp });
//        }
//
//        public virtual void AddEmployees(IEnumerable<Employee> emps)
//        {
//            var employees = emps as Employee[] ?? emps.ToArray();
//            if (emps == null || !employees.Any()) return;
//
//            employees.ForEach(AddEmployee);
//        }

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
            Property(x => x.Name);
            //            Bag(x => x.Employees, cm =>
            //              {
            //                  cm.Access(Accessor.Field);
            //                  cm.Key(km =>
            //                  {
            //                      km.Column("Store_id");
            //                  });
            //                  cm.Cascade(Cascade.All);
            //                  cm.Inverse(true);
            //              }, m => m.OneToMany());
            //            DynamicUpdate(true);
        }
    }
}