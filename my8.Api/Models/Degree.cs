using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace my8.Api.Models
{
    public class Degree
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
		public int Value { get; set; }
        public string Name { get; set; }
    }
}

