using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models.Mongo
{
    public class Location
    {
        public Location() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
	    public int Value { get; set; }
        public string Display { get; set; }
    }
}

