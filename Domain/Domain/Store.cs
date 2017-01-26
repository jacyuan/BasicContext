using System.Collections.Generic;
using NHibernate.Util;
using Enumerable = System.Linq.Enumerable;
using System.Linq;

namespace Domain.Domain
{
    public class Store
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IEnumerable<Employee> Employees { get; set; }

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

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}