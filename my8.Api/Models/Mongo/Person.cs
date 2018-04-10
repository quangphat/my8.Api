using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace my8.Api.Models.Mongo
{
    public class Person
    {
        public Person() { }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public double Rate { get; set; }
        public string WorkAs { get; set; }
    }
}
