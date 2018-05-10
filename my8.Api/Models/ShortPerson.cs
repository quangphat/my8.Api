using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models
{
    public class ShortPerson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string AuthorId { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public string Url { get; set; }
        public int AuthorTypeId { get; set; }
    }
}
