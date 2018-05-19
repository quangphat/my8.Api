using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace my8.Api.Models
{
    public class ReplyComment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Author ReplyBy { get; set; }
        public string ReplyTime { get; set; }
        public string EditedTime { get; set; }
        public int Likes { get; set; }
        public string PostId { get; set; }
        public string CommmentId { get; set; }
        public string Content { get; set; }
    }
}
