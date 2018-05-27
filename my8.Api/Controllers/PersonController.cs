using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using my8.Api.IBusiness;
using my8.Api.Infrastructures;
using my8.Api.Models;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class PersonController : BaseController
    {
        IPersonBusiness m_PersonBusiness;
        public PersonController(CurrentProcess process, IPersonBusiness personBusiness):base(process)
        {
            m_PersonBusiness = personBusiness;
        }
        [HttpPost]
        [Route("api/account/login")]
        public async Task<IActionResult> Login([FromBody] Person model)
        {
            Person account = await m_PersonBusiness.Login(model);
            return ToResponse(account);
        }
        [HttpPost]
        [Route("api/person/create")]
        public async Task<IActionResult> Create([FromBody] Person model)
        {
            model = new Person();
            model.DisplayName = "Lind Diệu";
            model.WorkAs = "HR";
            model.Rate = 7;

            model.Experience = 2;
            model.IndustryTags = new List<Industry>() {
                new Industry(){ Code="hr",Display="HR" }
            };
            Person person =  await m_PersonBusiness.Create(model);
            return Json(person);
        }
        [HttpGet]
        [Route("api/person/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Person person = await m_PersonBusiness.Get(id);
            return Json(person);
        }
        [HttpGet]
        [Route("api/person/GetPersonSql/{id}")]
        public async Task<IActionResult> GetInSqlById(string id)
        {
            Person person = await m_PersonBusiness.GetSql(id);
            return Json(person);
        }
        [HttpPut]
        [Route("api/person/update")]
        public async Task<IActionResult> UpdatePerson([FromBody] Person person)
        {
            bool rs = await m_PersonBusiness.Update(person);
            return Json(rs);
        }
        [HttpGet]
        [Route("api/person/search/{searchstr}/{skip}/{limit}/{currentPersonId}")]
        public async Task<IActionResult> Search(string searchstr,int skip,int limit,string currentPersonId)
        {
            List<PersonAllin> lstPerson = await m_PersonBusiness.Search(currentPersonId, searchstr, skip, limit);
            return Json(lstPerson);
        }
        [HttpGet]
        [Route("api/person/FollowingPage/{id}")]
        public async Task<IActionResult> GetFollowingPage(string id)
        {
            List<Page> lstPage = await m_PersonBusiness.GetFollowingPage(id);
            return ToResponse(lstPage);
        }
        [HttpPost]
        [Route("api/person/followpage/{personId}/{pageId}")]
        public async Task<IActionResult> FollowPage(string personId,string pageId)
        {
            bool result = await m_PersonBusiness.FollowPage(personId,pageId);
            return Json(result);
        }
        [HttpPost]
        [Route("api/person/unfollowpage/{personId}/{pageId}")]
        public async Task<IActionResult> UnFollowPage(string personId, string pageId)
        {
            bool result = await m_PersonBusiness.UnFollowPage(personId, pageId);
            return Json(result);
        }
        [HttpPost]
        [Route("api/person/interacttopage/{personId}/{pageId}")]
        public async Task<IActionResult> InteractToPage(string personId, string pageId)
        {
            bool result = await m_PersonBusiness.InteractToPage(personId, pageId);
            return Json(result);
        }

        [HttpPost]
        [Route("api/person/addfriend/{sendbyId}/{sendtoId}")]
        public async Task<IActionResult> AddFriend(string sendbyId, string sendtoId)
        {
            bool result = await m_PersonBusiness.AddFriend(sendbyId, sendtoId);
            return Json(result);
        }
        [HttpPost]
        [Route("api/person/unfriend/{sendbyId}/{sendtoId}")]
        public async Task<IActionResult> UnFriend(string sendbyId, string sendtoId)
        {
            bool result = await m_PersonBusiness.UnFriend(sendbyId, sendtoId);
            return Json(result);
        }
        [HttpGet]
        [Route("api/person/get-allfriend/{id}")]
        public async Task<IActionResult> GetAllFriends(string id)
        {
            List<PersonAllin> lstFriend = await m_PersonBusiness.GetAllFriend(id);
            return Json(lstFriend);
        }
        [HttpGet]
        [Route("api/person/get-commonfriend/{id}/{friendId}")]
        public async Task<IActionResult> GetAllFriends(string id,string friendId)
        {
            List<Person> lstFriend = await m_PersonBusiness.FindCommondFriend(id,friendId);
            return Json(lstFriend);
        }
        [HttpPost]
        [Route("api/person/join-Community/{id}/{CommunityId}")]
        public async Task<IActionResult> JoinCommunity(string id,string CommunityId)
        {
            bool result = await m_PersonBusiness.JoinCommunity(id, CommunityId);
            return Json(result);
        }

        [HttpGet]
        [Route("api/person/get-recommendpages/{id}/{limit}")]
        public async Task<IActionResult> GetRecommendPages(string id, int limit)
        {
            List<Page> pages = await m_PersonBusiness.GetRecommendPage(id, limit);
            return Json(pages);
        }
        [HttpGet]
        [Route("api/person/getTopInteractiveFriends/{personId}")]
        public async Task<IActionResult> GetTopInteractiveFriends(string personId)
        {
            List<PersonAllin> shortPeople = await m_PersonBusiness.GetTopFriendInteractive(personId);
            return ToResponse(shortPeople);
        }
    }
}