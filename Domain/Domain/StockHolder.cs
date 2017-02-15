using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class StockHolder : StoreOwner
    {
        public StockHolder()
        {

        }
    }

    public class StockHolderMap : SubclassMapping<StockHolder>
    {
        public StockHolderMap()
        {
            DiscriminatorValue(EmployeeTypeEnum.StockHolder);
        }
    }
}