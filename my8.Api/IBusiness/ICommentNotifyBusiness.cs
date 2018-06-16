using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface ICommentNotifyBusiness
    {
        Task<CommentNotify> Create(CommentNotify commentnotify);
        Task<CommentNotify> Get(string commentnotifyId);
        Task<bool> Update(CommentNotify commentnotify);
        Task<bool> Delete(string id);
        Task<List<CommentNotify>> Search(string searchStr);
    }
}
