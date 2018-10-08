using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.ISmartCenter
{
    public interface IFeedSmart
    {
        Task<bool> InitBroadcast();
        Task<bool> BroadcastToPerson(StatusPost post);
        Task<bool> BroadcastToPerson(JobPost post);
        Task<List<Feed>> GetPosts(int skip);
        Task<List<Feed>> InjectGrayMatter(List<PostBroadcastPerson> postBroadcasts, List<Feed> postAllTypes);
    }
}
