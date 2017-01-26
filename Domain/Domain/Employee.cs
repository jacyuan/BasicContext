using System.Collections.Generic;

namespace Domain.Domain
{
    public class Employee
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual GenderEnum Gender { get; set; }
        public virtual Store Store { get; set; }

        public override string ToString()
        {
            return $"{Name} {Gender} {Age} {Store.Name}";
        }

        private sealed class EmployeeEqualityComparer : IEqualityComparer<Employee>
        {
            public bool Equals(Employee x, Employee y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return string.Equals(x.Name, y.Name) && x.Age == y.Age && x.Gender == y.Gender && Equals(x.Store, y.Store);
            }

            public int GetHashCode(Employee obj)
            {
                unchecked
                {
                    var hashCode = (obj.Name != null ? obj.Name.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ obj.Age;
                    hashCode = (hashCode * 397) ^ (int)obj.Gender;
                    hashCode = (hashCode * 397) ^ (obj.Store != null ? obj.Store.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        private static readonly IEqualityComparer<Employee> EmployeeComparerInstance = new EmployeeEqualityComparer();

        public static IEqualityComparer<Employee> EmployeeComparer
        {
            get { return EmployeeComparerInstance; }
        }
    }
}