using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class PostBroadcastPerson
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string PostId { get; set; }
        public PostTypeEnum PostType { get; set; }
        public string KeyTime { get; set; }
        public int InteractivePoint { get; set; }
    }
    public class PostBroadcastPersonHidden
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string PostId { get; set; }
        public PostTypeEnum PostType { get; set; }
        public string PostTime { get; set; }
        public int InteractivePoint { get; set; }
    }
}
