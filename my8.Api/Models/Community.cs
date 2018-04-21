using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class Community
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        [JsonProperty(PropertyName = "Id")]
        public string CommunityId { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public double Rate { get; set; }
        public CommunityPrivacy Privacy { get; set; }
        public int Joins { get; set; }
        public int CommunityIPoint { get; set; } //Community interaction point
        public string Title { get; set; }
    }
}
