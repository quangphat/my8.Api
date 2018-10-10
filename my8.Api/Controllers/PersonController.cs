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
    [Route("Persons")]
    public class PersonController : BaseController
    {
        IPersonBusiness m_PersonBusiness;
        public PersonController(CurrentProcess process, IPersonBusiness personBusiness):base(process)
        {
            m_PersonBusiness = personBusiness;
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] Person model)
        {
            Person account = await m_PersonBusiness.Login(model);
            return ToResponse(account);
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Person model)
        {
            model = new Person();
            model.DisplayName = "Lind Diệu";
            model.WorkAs = "HR";
            model.Rate = 7;

            model.Experience = 2;
            model.JobFunctionTags = new List<JobFunction>() {
                new JobFunction(){ Code="hr",Display="HR" }
            };
            Person person =  await m_PersonBusiness.Create(model);
            return Json(person);
        }
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Person person = await m_PersonBusiness.Get(id);
            return Json(person);
        }
        [HttpGet]
        [Route("GetbyProfileName/{profileName}")]
        public async Task<IActionResult> GetByUrl(string profileName)
        {
            Person person = await m_PersonBusiness.GetByProfileName(profileName);
            return ToResponse(person);
        }
        [HttpGet]
        [Route("GetPersonSql/{id}")]
        public async Task<IActionResult> GetInSqlById(string id)
        {
            Person person = await m_PersonBusiness.GetSql(id);
            return Json(person);
        }
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdatePerson([FromBody] Person person)
        {
            bool rs = await m_PersonBusiness.Update(person);
            return Json(rs);
        }
        [HttpGet]
        [Route("search/{searchstr}/{skip}/{limit}/{currentPersonId}")]
        public async Task<IActionResult> Search(string searchstr,int skip,int limit,string currentPersonId)
        {
            List<PersonAllin> lstPerson = await m_PersonBusiness.Search(currentPersonId, searchstr, skip, limit);
            return Json(lstPerson);
        }
        [HttpGet]
        [Route("FollowingPages")]
        public async Task<IActionResult> GetFollowingPage()
        {
            List<Page> lstPage = await m_PersonBusiness.GetFollowingPage();
            return ToResponse(lstPage);
        }
        [HttpPost]
        [Route("followpage/{personId}/{pageId}")]
        public async Task<IActionResult> FollowPage(string personId,string pageId)
        {
            bool result = await m_PersonBusiness.FollowPage(personId,pageId);
            return Json(result);
        }
        [HttpPost]
        [Route("unfollowpage/{personId}/{pageId}")]
        public async Task<IActionResult> UnFollowPage(string personId, string pageId)
        {
            bool result = await m_PersonBusiness.UnFollowPage(personId, pageId);
            return Json(result);
        }
        [HttpPost]
        [Route("interacttopage/{personId}/{pageId}")]
        public async Task<IActionResult> InteractToPage(string personId, string pageId)
        {
            bool result = await m_PersonBusiness.InteractToPage(personId, pageId);
            return Json(result);
        }

        [HttpPost]
        [Route("addfriend/{sendbyId}/{sendtoId}")]
        public async Task<IActionResult> AddFriend(string sendbyId, string sendtoId)
        {
            bool result = await m_PersonBusiness.AddFriend(sendbyId, sendtoId);
            return Json(result);
        }
        [HttpPost]
        [Route("unfriend/{sendbyId}/{sendtoId}")]
        public async Task<IActionResult> UnFriend(string sendbyId, string sendtoId)
        {
            bool result = await m_PersonBusiness.UnFriend(sendbyId, sendtoId);
            return Json(result);
        }
        [HttpGet]
        [Route("getAllfriend/{id}")]
        public async Task<IActionResult> GetAllFriends(string id)
        {
            List<PersonAllin> lstFriend = await m_PersonBusiness.GetAllFriend(id);
            return Json(lstFriend);
        }
        [HttpGet]
        [Route("getCommonfriend/{id}/{friendId}")]
        public async Task<IActionResult> GetAllFriends(string id,string friendId)
        {
            List<Person> lstFriend = await m_PersonBusiness.FindCommondFriend(id,friendId);
            return Json(lstFriend);
        }
        [HttpPost]
        [Route("{personId}/joinCommunity/{CommunityId}")]
        public async Task<IActionResult> JoinCommunity(string personId, string CommunityId)
        {
            bool result = await m_PersonBusiness.JoinCommunity(personId, CommunityId);
            return Json(result);
        }

        [HttpGet]
        [Route("{personId}/getRecommendpages/{limit}")]
        public async Task<IActionResult> GetRecommendPages(string personId, int limit)
        {
            List<Page> pages = await m_PersonBusiness.GetRecommendPage(personId, limit);
            return Json(pages);
        }
        [HttpGet]
        [Route("TopInteractiveFriends")]
        public async Task<IActionResult> GetTopInteractiveFriends()
        {
            List<PersonAllin> shortPeople = await m_PersonBusiness.GetTopFriendInteractive();
            return ToResponse(shortPeople);
        }
    }
}