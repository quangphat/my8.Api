using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IPostBroadcastPersonBusiness
    {
        Task<bool> BroadcastToPerson(StatusPost post);
        Task<bool> BroadcastToPerson(JobPost post);
        Task<List<PostBroadcastPerson>> GetByPerson(string personId);
    }
}
