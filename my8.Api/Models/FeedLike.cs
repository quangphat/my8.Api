using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;
using Newtonsoft.Json;


namespace my8.Api.Models
{
    public class FeedLike
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
        public PostTypeEnum FeedType { get; set; }
        public string FeedId { get; set; }
        public Author Author { get; set; }
        public bool Liked { get; set; }
        public string BroadCastId { get;set; }
    }
}
