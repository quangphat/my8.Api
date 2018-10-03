using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class Experience
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
        public string CompanyName { get; set; }
        public Location Location { get; set; }
        public DateTime FromDate { get; set; }
        public long FromDateUnix { get; set; }
        public bool isCurrentlyWorkHere { get; set; }
        public DateTime? ToDate { get; set; }
        public Industry Industry { get; set; }
        public string WorkAs { get; set; }
        public string Description { get; set; }
        public string PersonId { get; set; }
        public long CreatedTime { get; set; }
        public long UpdatedTime { get; set; }
    }
}
