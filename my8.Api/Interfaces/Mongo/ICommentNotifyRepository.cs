using my8.Api.Models;
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
        Task<List<CommentNotify>> Search(string searchStr);
    }
}

