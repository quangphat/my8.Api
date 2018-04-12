using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models
{
    public class Company
    {
        public Company() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
	    public int Value { get; set; }
        public string Display { get; set; }
        public Location Location { get; set; }
    }
}

