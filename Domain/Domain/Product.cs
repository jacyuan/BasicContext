using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Domain.Domain
{
    public class Product : Entity
    {
        public virtual string Name { get; set; }
    }

    public class ProductMap : ClassMapping<Product>
    {
        public ProductMap()
        {
            Id(x => x.Id, map => map.Generator(Generators.Identity));
            Property(x => x.Name);
        }
    }
}