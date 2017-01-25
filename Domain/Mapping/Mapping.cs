using Domain.Domain;
using FluentNHibernate.Mapping;

namespace Domain.Mapping
{
    public class StoreMap : ClassMap<Store>
    {
        public StoreMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
        }
    }
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);
            Map(x => x.Age);
            References(x => x.Store, "Store_id").Not.Nullable();
            Map(x => x.Gender).CustomType<GenderEnum>();
        }
    }
}