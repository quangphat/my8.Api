

namespace my8.Api.Models.Neo4j
{
    public class Person
    {
        public int id { get; set; }
        public string DisplayName { get; set; }
        public string WorkAs { get; set; }
        public string Company { get; set; }
        public double Rate { get; set; }//Đánh giá
        //public int commonFriend { get; set; }
        //public FriendEdge Friend { get; set; }
        //public FollowEdge FollowPage { get; set; }
    }
}
