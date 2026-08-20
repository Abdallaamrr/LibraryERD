namespace LibraryERD.Domain
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public Customers? customer { get; set; }
        public string? PostalCode { get; set; }
    }
}
