namespace Domain.Domain
{
    public class Store
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}