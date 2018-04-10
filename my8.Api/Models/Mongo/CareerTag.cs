using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace my8.Api.Models.Mongo
{
    public class CareerTag
    {
        public CareerTag() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
	    public string Code { get; set; }
        public string Display { get; set; }
    }
}

