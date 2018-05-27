
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace my8.Api.Interfaces.Neo4j
{
    public interface IPersonRepository
    {
        Task<bool> Create(Person user);
        Task<bool> Update(Person person);
        Task<IEnumerable<PersonAllin>> GetFriends(string currentPersonId);
        Task<IEnumerable<Person>> FindCommonFriend(string p1, string friend);
        Task<bool> AddFriend(string sendBy, string sentTo);
        Task<bool> UnFriend(string sendBy, string sentTo);
        Task<IEnumerable<PersonAllin>> GetTopFriendInteractive(string personId, int top);// most top interactive
        Task<PersonAllin> FindParticularPerson(Person currentPerson, Person findingPerson);
        Task<IEnumerable<PersonAllin>> Search(string currentPersonId, string searchStr,int skip,int limit);
        Task<bool> InteractionToFriend(Person currentPerson, Person friend);

        Task<IEnumerable<Page>> GetFollowingPage(string userId);
        Task<bool> FollowPage(string currentPersonId, string pageId);
        Task<bool> UnFollowPage(string currentPersonID, string pageId);
        Task<bool> InteractToPage(string currentPersonId, string pageId);
        Task<IEnumerable<Page>> GetRecommendPage(string personId, int limit);


        Task<IEnumerable<Community>> GetJoiningCommunitys(string userId);
        Task<bool> JoinCommunity(string currentPersonId, string CommunityId);
        Task<bool> OutCommunity(string currentPersonId, string teamId);
        Task<bool> InteractToCommunity(string currentPersonId, string CommunityId);
    }
}
