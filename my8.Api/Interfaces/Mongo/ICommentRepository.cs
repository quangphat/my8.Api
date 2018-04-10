
using my8.Api.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetAll(StatusPost post);
        Task PostComment(Comment comment);
        Task DeleteComment(Comment comment);
        Task<Comment> EditComment(Comment comment);
    }
}
