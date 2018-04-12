using MongoDB.Bson;
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IJobPostRepository
    {
        Task<IEnumerable<JobPost>> Gets(int size);
        Task<JobPost> Get(ObjectId id);
        Task Post(JobPost post);

    }
}
