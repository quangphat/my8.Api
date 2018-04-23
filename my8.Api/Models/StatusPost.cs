using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class StatusPost
    {
        public StatusPost() { }
        public StatusPost(string id)
        {
            this.Id = id;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PostTime { get; set; }
        public string Content { get; set; }
        public Actor PostBy { get; set; }// có thể là người/trang post, người like, người comment,...
        public string EditedTime { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; } //Số comment
        public int Shares { get; set; }
        public int Views { get; set; }
        public string[] Images { get; set; }
        public List<Person> PersonTags { get; set; }   
        public bool IsShareExperience { get; set; }//Gắn thẻ là bài đăng chia sẻ kiến thức
        public bool IsAds { get; set; }//bài đăng quảng cáo.
        public int Privacy { get; set; }
        [BsonIgnore]
        public int PostType { get; } = 1;
    }
}
