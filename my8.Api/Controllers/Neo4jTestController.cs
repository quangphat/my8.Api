using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.Interfaces.Neo4j;
using my8.Api.Models.Neo4j;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class Neo4jTestController : Controller
    {
        IPageRepository pageRepository;
        IPersonRepository PersonRepository;
        public Neo4jTestController(IPageRepository pageRepo,IPersonRepository userRepo)
        {
            pageRepository = pageRepo;
            PersonRepository = userRepo;
        }
        [HttpGet]
        [Route("api/Person")]
        public async Task<Person> GetPerson()
        {
            //userRepository = new PersonRepository();
            //Person user = await userRepository.FindPerson(1);
            return null;
        }
        [HttpPost]
        [Route("/api/Person/Post")]
        public async Task PostPerson()
        {
            Person user = new Person();
            user.id = 2;
            user.DisplayName = "mộng nhàn";
            await PersonRepository.CreatePerson(user);
        }
        [HttpPost]
        [Route("/api/Person/CreateFriend")]
        public async Task CreateFriend()
        {
            Person sendBy = new Person();
            sendBy.id = 5;
            Person sendTo = new Person();
            sendTo.id = 1;

            await PersonRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/Person/FindCommonFriend")]
        public async Task FindCommonFriend()
        {
            Person sendBy = new Person();
            sendBy.id = 1;
            Person sendTo = new Person();
            sendTo.id = 3;
            IEnumerable<Person> lstPerson = await PersonRepository.FindCommonFriend(sendBy, sendTo);
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/Person/GetFriends")]
        public async Task GetFriends()
        {
            Person sendBy = new Person();
            sendBy.id = 1;
            IEnumerable<PersonAllin> lstPerson = await PersonRepository.GetFriends(sendBy.id);
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/Person/FindParticularPerson")]
        public async Task FindParticularPerson()
        {
            Person sendBy = new Person();
            sendBy.id = 1;
            Person find = new Person() { id = 2 };
            PersonAllin user = await PersonRepository.FindParticularPerson(sendBy, find);
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/Person/FindPersons")]
        public async Task FindPersons()
        {
            Person sendBy = new Person();
            sendBy.id = 1;
            Person find = new Person() { id = 2 };
            IEnumerable<PersonAllin> users = await PersonRepository.FindPersons(sendBy, "la");
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/Person/GetTopInteractive")]
        public async Task GetTopInteractive()
        {
            Person sendBy = new Person();
            sendBy.id = 1;
            Person find = new Person() { id = 2 };
            IEnumerable<PersonAllin> users = await PersonRepository.GetTopFriendInteractive(sendBy, 2);
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/Person/GetFollowedPage")]
        public async Task GetFollowedPage()
        {
            Person sendBy = new Person();
            sendBy.id = 1;
            Person find = new Person() { id = 2 };
            IEnumerable<Page> pages = await PersonRepository.GetFollowedPage(sendBy);
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/Person/GetJoinedClub")]
        public async Task GetJoinedClub()
        {
            Person sendBy = new Person();
            sendBy.id = 1;
            Person find = new Person() { id = 2 };
            IEnumerable<Club> teams = await PersonRepository.GetJoinedClub(sendBy);
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpGet]
        [Route("/api/page/search/{searchStr}")]
        public async Task<IEnumerable<Page>> SearchPages(string searchStr)
        {
            IEnumerable<Page> pages = await pageRepository.Search(searchStr,20);
            return pages;
            //await userRepository.AddFriend(sendBy, sendTo);
        }
        [HttpPost]
        [Route("/api/page/create")]
        public async Task CreatePage()
        {
            for (int i = 4; i < 1000; i++)
            {
                Page p = new Page();
                p.Id = i;
                p.DisplayName = $"Page #{i}";
                await pageRepository.Create(p);
            }
            //await userRepository.AddFriend(sendBy, sendTo);
        }
    }
}