
using my8.Api.Models.Neo4j;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Neo4j
{
    public interface IPersonRepository
    {
        Task CreatePerson(Person user);
        Task<IEnumerable<PersonAllin>> GetFriends(int Id);
        Task<IEnumerable<Person>> FindCommonFriend(Person user, Person friend);
        Task AddFriend(Person user,Person friend);
        Task<IEnumerable<PersonAllin>> GetTopFriendInteractive(Person currentUSer, int top);// most top interactive
        Task<PersonAllin> FindParticularPerson(Person currentPerson, Person findingPerson);
        Task<IEnumerable<PersonAllin>> FindPersons(Person currentUSer, string searchStr);
        Task UnFriend(Person currentPerson, Person friend);
        Task<bool> InteractionToFriend(Person currentPerson, Person friend);

        Task<IEnumerable<Page>> GetFollowedPage(Person user);
        Task<bool> FollowPage(Person currentPerson, Page page);
        Task<bool> UnFollowPage(Person currentPerson, Page page);
        Task<bool> InteractToPage(Person currentPerson, Page page);

        Task<IEnumerable<Club>> GetJoinedClub(Person user);
        Task<bool> JoinClub(Person currentPerson, Club team);
        Task<bool> OutClub(Person currentPerson, Club team);
    }
}
