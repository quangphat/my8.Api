using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using my8.Api.my8Enum;

namespace my8.Api.Controllers
{
    [Produces("application/json")]
    public class MongoTestController : Controller
    {
        IStatusPostRepository statusPostRepository;
        ICommentRepository commentRepository;
        IJobPostRepository jobPostRepository;
        public MongoTestController(IStatusPostRepository _statusPostRepository, ICommentRepository _commentRepo, IJobPostRepository _jobPostRepo)
        {
            statusPostRepository = _statusPostRepository;
            commentRepository = _commentRepo;
            jobPostRepository = _jobPostRepo;
        }
        [HttpGet]
        [Route("api/StatusPost")]
        public async Task GetAllPost()
        {
            IEnumerable<StatusPost> StatusPosts = await statusPostRepository.Gets(new string[] {string.Empty});
        }
        [HttpPost]
        [Route("api/statuspost/create-status-post")]
        public async Task CreateStatusPost([FromBody] StatusPost post)
        {
            await statusPostRepository.Post(post);
        }
        [HttpPost]
        [Route("api/PostComment")]
        public async Task PostComment()
        {

            Actor user = new Actor();
            user.DisplayName = "mộng nhàn";
            Comment comment = new Comment();
            comment.Commentator = user;
            comment.PostId = new MongoDB.Bson.ObjectId("5aa6a58f81b86d4a746f8b6b");
            comment.Content = "gì vậy nhóc?";
            comment.CommentTime = DateTime.UtcNow;
            comment.Likes = 0;
            comment.Replies = 0;
            await commentRepository.PostComment(comment);
        }
        [HttpGet]
        [Route("api/GetComment")]
        public async Task<IActionResult> GetComment()
        {

            StatusPost post = new StatusPost();
            IEnumerable<Comment> comments = await commentRepository.GetAll(post);
            return Json(comments);
        }
        [HttpPost]
        [Route("api/updatePost")]
        public async Task UpdatePost()
        {
            StatusPost post = new StatusPost();
            await statusPostRepository.UpdatePost(post);
        }
        [HttpPost]
        [Route("api/jobPost")]
        public async Task JobPost()
        {
            JobPost post = new JobPost();
            post.PostTime = DateTime.UtcNow;
            post.PostBy = new Actor();
            post.PostBy.DisplayName = "mộng nhàn";
            post.PostBy.ActorTypeId = (int)ActorTypeEnum.Person;
            post.Content = "Tuyển nhân viên kế toán";
            post.Applies = 2;
            post.Comments = 3;
            post.Likes = 10;

            await jobPostRepository.Post(post);
        }
        [HttpPost]
        [Route("api/PostMany")]
        public async Task PostMany()
        {
            List<StatusPost> lstStatusPost = new List<StatusPost>();
            Actor user = new Actor();
            user.DisplayName = "Quang Phát";
            for (int i = 0; i < 1000001; i++)
            {
                StatusPost post = new StatusPost();
                post.Comments = 4;
                post.Likes = 10;
                post.Shares = 1;
                post.Views = 15;
                post.PostTime = DateTime.UtcNow;
                post.Images = new string[5];
                post.Content = "the status post number #" + i.ToString();
                post.PostBy = user;
                //lstStatusPost.Add(post);
                await statusPostRepository.Post(post);
            }
            //await StatusPostRepository.Post(lstStatusPost);
        }
        [HttpGet]
        [Route("api/GetStatusPost/{size}/{skip}")]
        public async Task<IEnumerable<StatusPost>> GetStatusPost(int size, int skip)
        {
            IEnumerable<StatusPost> lstStatusPost = await statusPostRepository.Gets(new string[] { string.Empty});
            //return Mapper.Map<IEnumerable<StatusPost>>(lstStatusPost);
            return lstStatusPost;
        }
    }
}