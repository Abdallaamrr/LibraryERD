namespace LibraryERD.Domain
{
    public class Customers
    {
        public int CustomerId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime SignUpDate { get; set; }
    }
}
