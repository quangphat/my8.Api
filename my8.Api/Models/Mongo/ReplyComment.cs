using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace my8.Api.Models.Mongo
{
    public class ReplyComment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public Actor ReplyBy { get; set; }
        public DateTime ReplyTime { get; set; }
        public DateTime EditedTime { get; set; }
        public int Likes { get; set; }
        public ObjectId PostId { get; set; }
        public ObjectId CommmentId { get; set; }
        public string Content { get; set; }
    }
}
