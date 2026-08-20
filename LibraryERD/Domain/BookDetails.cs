using System.Text.Json.Serialization;

namespace LibraryERD.Domain
{
    public class BookDetails
    {
        public int BookId { get; set; }
        public int PageCount { get; set; }
        public string? Language { get; set; }
        public string? Description { get; set; }
        [JsonIgnore]
        public Books? Book { get; set; }
    }
}
