namespace LibraryERD.Domain
{
    public class Books
    {
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
        public string? ISBN { get; set; }
        public int price { get; set; }
        public DateTime PublishedDate { get; set; }
        public int PublisherId { get; set; }

    }
}
