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
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("Id")]
        public string Id { get; set; }
        public string AuthorId { get; set; }
        public string AuthorDisplayName { get; set; }
        public AuthorType AuthorType { get; set; }
        public NotifyType NotifyType { get; set; }
        public string CommentId { get; set; }
        public string FeedId { get; set; }
        public PostType FeedType { get; set; }
        public string ReceiverId { get; set; }
        public AuthorType ReceiverType { get; set; }
        public long NotifyTimeUnix { get; set; }
        public long OthersCommentator { get; set; }
    }
}

