using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;


namespace my8.Api.Models
{
    public class LastPostBroadCast
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public AuthorTypeEnum AuthorType { get; set; }
        public string PersonId { get; set; }
        public string LastPostIdToPerson { get; set; }
        public PostTypeEnum PostType { get; set; }
        public long LastPostTimeToPerson { get; set; }
    }
}
