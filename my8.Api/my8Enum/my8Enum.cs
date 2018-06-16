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
}
