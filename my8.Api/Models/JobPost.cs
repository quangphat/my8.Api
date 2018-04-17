using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class JobPost : StatusPost
    {
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
        public SeniorityLevel Seniority { get; set; }
        public EmploymentType EmploymentType { get; set; }
        public int MinExperience { get; set; }
        public int MaxExperience { get; set; }
    }
}
