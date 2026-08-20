namespace LibraryERD.Domain
{
    public class Reviews
    {
        public int CustomerId { get; set; }
        public Customers? Customers { get; set; }
        public int BookId { get; set; }
        public Books? Books { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public int Rating { get; set; } // Assuming rating is an integer value
    }
}
