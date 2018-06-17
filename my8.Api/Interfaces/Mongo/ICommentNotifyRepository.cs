using my8.Api.Models;
using my8.Api.my8Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ICommentNotifyRepository
    {
        Task<string> Create(CommentNotify commentnotify);
        Task<CommentNotify> Get(string id);
        Task<bool> Update(CommentNotify commentnotify);
        Task<bool> Delete(string id);
        Task<long> CountCommentator(string feedId, PostType postType, string exceptCommentatorId, AuthorType exceptAuthorType);
    }
}

