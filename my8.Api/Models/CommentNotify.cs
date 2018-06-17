using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class CommentNotify
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string Id { get; set; }
		public string CommentatorId { get; set; }
        public AuthorType CommentatorType { get; set; }
        public string FeedId { get; set; }
        public PostType FeedType { get; set; }
    }
}

