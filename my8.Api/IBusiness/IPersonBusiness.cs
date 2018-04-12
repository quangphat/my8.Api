using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IPersonBusiness
    {
        Task<Person> Create(Person person);
        Task<Person> Get(string id);
        Task<Person> GetSql(string id);
        Task<bool> Update(Person person);
        Task<List<PersonAllin>> Search(string currentPersonId, string searchStr, int skip, int limit);
        Task<bool> InteractToFriend(Person current, Person Friend);
        Task<List<Person>> FindCommondFriend(string currentId, string friendId);
        Task<bool> AddFriend(string sendBy, string sendTo);
        Task<bool> UnFriend(string sendBy, string sendTo);
        Task<List<PersonAllin>> GetAllFriend(string personId);
        Task<List<PersonAllin>> GetTopFriendInteractive(Person currentPerson, int top);
        Task<bool> FollowPage(string currentPersonId, string pageId);
        Task<bool> UnFollowPage(string currentPersonId, string pageId);
        Task<bool> InteractToPage(string currentPersonId, string pageId);
        Task<List<Page>> GetFollowingPage(string userId);
        //Task<IEnumerable<Club>> GetJoinedClub(Model.Person user);
    }
}
