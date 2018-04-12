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
        Task<List<PersonAllin>> Search(Person current,string searchStr, int skip, int limit);
        Task<bool> InteractToFriend(Person current, Person Friend);
        Task<List<Person>> FindCommondFriend(Person current, Person friend);
        Task<bool> AddFriend(Person sendBy, Person sendTo);
        Task<List<PersonAllin>> GetAllFriend(string personId);
        Task<List<PersonAllin>> GetTopFriendInteractive(Person currentPerson, int top);
        Task<bool> FollowPage(Person currentPerson, Page page);
        Task<bool> UnFollowPage(Person currentPerson, Page page);
        Task<bool> InteractToPage(Person currentPerson, Page page);
        Task<List<Page>> GetFollowingPage(Person user);
        //Task<IEnumerable<Club>> GetJoinedClub(Model.Person user);
    }
}
