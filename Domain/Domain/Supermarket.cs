using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class Supermarket : Store
    {
        public virtual float Surface { get; set; }
    }

    //table per class
    public class SupermarketMap : JoinedSubclassMapping<Supermarket>
    {
        public SupermarketMap()
        {
            Key(km =>
            {
                km.Column("Id");
            });
            //not necessary
            //            Property(x => x.Surface);
        }
    }
}