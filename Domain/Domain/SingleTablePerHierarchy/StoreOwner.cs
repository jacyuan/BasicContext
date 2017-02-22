using System.Text;
using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class StoreOwner : Employee
    {
        public override string ToString()
        {
            var txt = new StringBuilder();
            txt.Append("StoreOwner : ").AppendLine().Append($"{Name}, {Gender}, {Age} years old");

            if (Store != null)
                txt.Append($", holds the {Store.Name} store");

            return txt.AppendLine().ToString();
        }
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