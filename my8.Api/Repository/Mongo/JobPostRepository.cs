using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models.Mongo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class JobPostRepository :MongoRepositoryBase, IJobPostRepository
    {
        IMongoCollection<StatusPost> collection;
        public JobPostRepository(IOptions<MongoConnection> mongoConnection) : base(mongoConnection)
        {
            collection = _db.GetCollection<StatusPost>("JobPost");
        }
        public async Task<IEnumerable<JobPost>> Gets(int size)
        {
            throw new NotImplementedException();
        }

        public async Task<JobPost> Get(ObjectId id)
        {
            throw new NotImplementedException();
        }

        public async Task Post(JobPost post)
        {
            await collection.InsertOneAsync(post);
        }
    }
}
