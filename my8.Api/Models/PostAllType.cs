﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using my8.Api.my8Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class PostAllType
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime PostTime { get; set; }
        public string Content { get; set; }
        public Actor PostBy { get; set; }// có thể là người/trang post, người like, người comment,...
        public DateTime EditedTime { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; } //Số comment
        public int Shares { get; set; }
        public int Views { get; set; }
        public string[] Images { get; set; }
        public List<Person> PersonTags { get; set; }
        public bool IsFindJob { get; set; }//Gắn thẻ là tìm việc
        public bool IsShareExperience { get; set; }//Gắn thẻ là bài đăng chia sẻ kiến thức
        public bool IsAds { get; set; }//bài đăng quảng cáo.
        public List<Industry> IndustryTags { get; set; }//Lĩnh vực công việc
        public List<Skill> SkillTags { get; set; }
        public List<Location> Locations { get; set; }

        public int MinExperience { get; set; }
        public int MaxExperience { get; set; }
        public List<Degree> Degrees { get; set; }
        public bool Active { get; set; }
        public int Privacy { get; set; }
        public SeniorityLevel Seniority { get; set; }
        public EmploymentType EmploymentType { get; set; }

        public int Applies { get; set; }
        public decimal MinSalary { get; set; }
        public decimal MaxSalary { get; set; }
        public string Title { get; set; }
        public string EmailToReceiveApply { get; set; }
        public PostTypeEnum PostType { get; set; }
    }
}
