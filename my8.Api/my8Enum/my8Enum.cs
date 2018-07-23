using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.my8Enum
{
    public enum PostType
    {
        StatusPost=1,
        JobPost=2,
        ExperiencePost=3
    }
    public enum ActionAsType
    {
        Person=1,
        Page=2,
        Community=3
    }
    public enum AuthorType
    {
        Person=1,
        Page=2,
        Community=3
    };
    public enum PostPrivacyType
    {
        All=1,
        Friend=2
    }
    public enum CommunityPrivacyType
    {
        All = 1,
        Member = 2
    }
    public enum LocationType
    {
        Country=1,
        City=2,
        District=3,
        Ward=4
    }
    public enum NotifyType
    {
        Comment=1,
        Like=2,
        Share=3
    }
    public enum NotificationTargetType
    {
        Person = 1,
        Page =2,
        Community=3
    }
    public enum EmploymentType
    {
        FullTime = 1,
        PartTime = 2,
        Contract = 3,
        Temporary = 4,
        Volunteer = 5,
        Internship = 6
    }
    public enum SeniorityLevelType
    {
        Fresher = 1,
        Junior = 2,
        Senior = 3,
        Director = 4
    }
}
