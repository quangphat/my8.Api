using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface INotificationBusiness
    {
        Task<Notification> Create(Notification commentnotify);
        Task<Notification> Get(string commentnotifyId);
        Task<bool> Update(Notification commentnotify);
        Task<bool> Delete(string id);
    }
}
