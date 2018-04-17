﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class StatusPost
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PostTime { get; set; }
        public string Content { get; set; }
        public Actor PostBy { get; set; }// có thể là người/trang post, người like, người comment,...
        public DateTime EditedTime { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; } //Số comment
        public int Shares { get; set; }
        public int Views { get; set; }
        public string[] Images { get; set; }
        public List<Person> PersonTags { get; set; }
        
        public bool IsShareExperience { get; set; }//Gắn thẻ là bài đăng chia sẻ kiến thức
        public bool IsAds { get; set; }//bài đăng quảng cáo.
        

        
        public int Privacy { get; set; }
        
    }
}
