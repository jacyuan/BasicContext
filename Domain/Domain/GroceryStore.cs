using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class GroceryStore : Store
    {
        public virtual bool IsFullDayStore { get; set; }
    }

    //table per class : subclasses share a same parent table, but have their own table
    public class GroceryStoreMap : JoinedSubclassMapping<GroceryStore>
    {
        public GroceryStoreMap()
        {
            Key(km =>
            {
                km.Column("Id");
            });

            Property(x => x.IsFullDayStore, m =>
              {
                  m.Column("FullDayStore");
              });
        }
    }
}