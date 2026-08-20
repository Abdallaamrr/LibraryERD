namespace LibraryERD.Domain
{
    public class ShippingAddresses
    {
        public int CustomerId { get; set; }
        public Customers? customer { get; set; }
        public string? AddressLine { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
    }
}
