﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.my8Enum
{
    public enum PostTypeEnum
    {
        StatusPost=1,
        JobPost=2,
        ExperiencePost=3
    }
    public enum AuthorTypeEnum
    {
        Person=1,
        Page=2,
        Community=3
    };
    public enum PostPrivacyEnum
    {
        All=1,
        Friend=2
    }
    public enum CommunityPrivacyEnum
    {
        All = 1,
        Member = 2
    }
}
