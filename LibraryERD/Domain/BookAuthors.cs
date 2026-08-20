namespace LibraryERD.Domain
{
    public class BookAuthors
    {
        public int BookId { get; set; }
        public Books? Books { get; set; }
        public int AuthorId { get; set; }
        public Authors? Authors { get; set; }
    }
}
