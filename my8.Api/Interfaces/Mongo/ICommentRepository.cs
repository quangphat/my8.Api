
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ICommentRepository
    {
        Task<string> Create(Comment comment);
        Task<Comment> Get(string commentId);
        Task<List<Comment>> GetByPost(StatusPost post,int skip,int limit);
        Task<List<Comment>> GetByPost(JobPost post, int skip, int limit);
        Task<bool> Delete(string commentId);
        Task<bool> Update(Comment comment);
    }
}
