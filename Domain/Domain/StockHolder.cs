using System.Text;
using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class StockHolder : StoreOwner
    {
        public StockHolder()
        {

        }

        public override string ToString()
        {
            var txt = new StringBuilder();
            txt.Append("StockHolder : ").AppendLine().Append($"{Name}, {Gender}, {Age} years old");

            if (Store != null)
                txt.Append($", holds the stocks of the {Store.Name} store");

            return txt.AppendLine().ToString();
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