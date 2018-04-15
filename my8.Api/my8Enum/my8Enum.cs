using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.my8Enum
{
    public enum PostTypeEnum
    {
        StatusPost=1,
        JobPost=2
    }
    public enum ActorTypeEnum
    {
        Person=1,
        Page=2,
        Club=3
    };
    public enum PostPrivaryEnum
    {
        All=1,
        Friend=2
    }
    public enum ClubPrivaryEnum
    {
        All = 1,
        Member = 2
    }
}
