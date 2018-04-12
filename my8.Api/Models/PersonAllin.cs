using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Models
{
    public class PersonAllin
    {
        public Person Person { get; set; }
        public FollowEdge FollowPage { get; set; }
        public FriendEdge Friend { get; set; }
        public int CommonFriend { get; set; }
    }
}
