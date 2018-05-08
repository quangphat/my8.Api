using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class JobPost
    {
        public JobPost() { }
        public JobPost(string id)
        {
            this.Id = id;
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string PostTime { get; set; }
        public string Content { get; set; }
        public ShortPerson PostBy { get; set; }// có thể là người/trang post, người like, người comment,...
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
        public bool IsFindJob { get; set; }//Gắn thẻ là tìm việc
        public int Applies { get; set; } //Số lượng apply
        public decimal MinSalary { get; set; } //Mức lương tối thiểu.
        public decimal MaxSalary { get; set; }
        public string Title { get; set; }
        public string EmailToReceiveApply { get; set; }//Gửi thông báo đến email khi có người apply
        public List<Industry> IndustryTags { get; set; }//Lĩnh vực công việc
        public List<Skill> SkillTags { get; set; }
        public List<Location> Locations { get; set; }
        public List<Degree> Degrees { get; set; }
        public bool Active { get; set; }
        public string Expire { get; set; }//Ngày hết hạn
        public SeniorityLevel Seniority { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int MinExperience { get; set; }
        public int MaxExperience { get; set; }
        [BsonIgnore]
        public int PostType { get; } = 2;
    }
}
