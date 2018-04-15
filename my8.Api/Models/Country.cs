using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models
{
    public class Country
    {
        public Country() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
	    public int Value { get; set; }
        public string Name { get; set; }
        public string NationalCode { get; set; }
    }
}

