using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;

namespace my8.Api.Models
{
    public class Location
    {
        public Location() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
        public string CountryCode { get; set; }
        public string DisplayName { get; set; }
        public LocationType LocationType { get; set; }
        public string[] KeySearchs { get; set; }
    }
}

