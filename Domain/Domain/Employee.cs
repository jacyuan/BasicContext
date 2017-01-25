using System;

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
    }
}