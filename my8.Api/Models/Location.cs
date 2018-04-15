using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models
{
    public class Location
    {
        public Location() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
        public Country Country { get; set; }
        public Province Province { get; set; }
        public District District { get; set; }
        public string Street { get; set; }
        public string Display { get; set; }
        
    }
}

