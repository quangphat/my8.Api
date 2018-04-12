
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace my8.Api.Interfaces.Neo4j
{
    public interface IPersonRepository
    {
        Task<bool> Create(Person user);
        Task<bool> Update(Person person);
        Task<IEnumerable<PersonAllin>> GetFriends(string Id);
        Task<IEnumerable<Person>> FindCommonFriend(string p1, string friend);
        Task<bool> AddFriend(string sendBy, string sentTo);
        Task<bool> UnFriend(string sendBy, string sentTo);
        Task<IEnumerable<PersonAllin>> GetTopFriendInteractive(Person currentUSer, int top);// most top interactive
        Task<PersonAllin> FindParticularPerson(Person currentPerson, Person findingPerson);
        Task<IEnumerable<PersonAllin>> FindPersons(string currentPersonId, string searchStr,int skip,int limit);
        Task<bool> InteractionToFriend(Person currentPerson, Person friend);

        Task<IEnumerable<Page>> GetFollowingPage(string userId);
        Task<bool> FollowPage(string currentPersonId, string pageId);
        Task<bool> UnFollowPage(string currentPersonID, string pageId);
        Task<bool> InteractToPage(string currentPersonId, string pageId);

        Task<IEnumerable<Club>> GetJoiningClubs(string userId);
        Task<bool> JoinClub(string currentPersonId, string clubId);
        Task<bool> OutClub(string currentPersonId, string teamId);
        Task<bool> InteractToClub(string currentPersonId, string clubId);
    }
}
