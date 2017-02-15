namespace Domain.Domain
{
    public class Address
    {
        public virtual int StreetNumber { get; set; }
        public virtual string Street { get; set; }
        public virtual string Country { get; set; }
        public virtual string ZipCode { get; set; }
    }
}