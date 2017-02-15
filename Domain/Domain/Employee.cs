using System.Text;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class Employee : Entity
    {
        public virtual string Name { get; set; }
        public virtual int Age { get; set; }
        public virtual GenderEnum Gender { get; set; }
        public virtual Store Store { get; set; }
        public virtual bool Employeed { get; set; }

        public override string ToString()
        {
            var txt = new StringBuilder();
            txt.Append("Employee : ").AppendLine().Append($"{Name}, {Gender}, {Age} years old");

            if (Store != null)
                txt.Append($", works in {Store.Name} store");

            return txt.ToString();
        }
    }

    public class EmployeeMap : ClassMapping<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));

            //not necessary
            //            Property(x => x.Name);
            //            Property(x => x.Age);
            //            Property(x => x.Gender);

            ManyToOne(x => x.Store, mom =>
            {
                mom.NotNullable(false);
                mom.Column("Store_id");
                //if necessary
                //                mom.Lazy(LazyRelation.Proxy);
            });

            Property(x => x.Employeed, map =>
              {
                  map.Formula("ISNULL(Store_id, 0)");
              });
        }
    }
}