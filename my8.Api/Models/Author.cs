using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models
{
    public class Author
    {
        public string AuthorId { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string Url { get; set; }
        public int AuthorTypeId { get; set; }
        public string WorkAs { get; set; }
        public string Company { get; set; }
    }
}
