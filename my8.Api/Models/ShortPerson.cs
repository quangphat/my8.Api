using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace my8.Api.Models
{
    public class ShortPerson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        [JsonProperty(PropertyName = "Id")]
        public string Id { get; set; }
		public string DisplayName { get; set; }
        public string Url { get; set; }
        public string Avatar { get; set; }
        public string WorkAs { get; set; }
        public string Company { get; set; }
        public double Rate { get; set; }//Đánh giá 
        public List<Industry> IndustryTags { get; set; }
        public string[] IndustriesCode { get; set; }
        public List<Skill> SkillTags { get; set; }
        public string[] SkillsCode { get; set; }
        public List<Location> Locations { get; set; }
        public string[] LocationId { get; set; }
    }
}

