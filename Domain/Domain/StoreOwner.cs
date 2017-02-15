using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class StoreOwner : Employee
    {
    }

    //attention the mapping class here "SubclassMapping"
    public class StoreOwnerMap : SubclassMapping<StoreOwner>
    {
        public StoreOwnerMap()
        {
            DiscriminatorValue(EmployeeTypeEnum.StoreOwner);
        }
    }
}