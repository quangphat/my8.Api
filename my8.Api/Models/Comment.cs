using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public ShortPerson Commentator { get; set; }
        public DateTime CommentTime { get; set; }
        public DateTime EditedTime { get; set; }
        public int Likes { get; set; }
        public int Replies { get; set; } //Lượt trả lời
        public string PostId { get; set; }
        public PostTypeEnum PostType { get; set; }
        public string Content { get; set; }
    }
}
