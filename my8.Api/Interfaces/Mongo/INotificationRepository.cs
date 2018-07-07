using my8.Api.Models;
using my8.Api.my8Enum;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface INotificationRepository
    {
        Task<string> Create(Notification commentnotify);
        Task<Notification> Get(string id);
        Task<Notification> Get(string feedId, PostType feedType, string authorId, AuthorType authorType);
        Task<Notification> GetByCodeExist(string code);
        Task<bool> Update(Notification commentnotify);
        Task<bool> Delete(string id);
        Task<long> CountOthers(string code, string actionAuthorId, string feedAuthorId);
    }
}

