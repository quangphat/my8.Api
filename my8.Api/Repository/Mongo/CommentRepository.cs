using Microsoft.Extensions.Options;
using MongoDB.Driver;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Mongo
{
    public class CommentRepository : MongoRepositoryBase,ICommentRepository
    {
        IMongoCollection<Comment> collection;
        public CommentRepository(IOptions<MongoConnection> mongoConnection) : base(mongoConnection)
        {
            collection = _db.GetCollection<Comment>("Comment");
        }

        public Task DeleteComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Comment> EditComment(Comment comment)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Comment>> GetAll(StatusPost post)
        {
            //FilterDefinition<StatusPost> filter = FilterDefinition<StatusPost>.Equals();
            //var filters = Builders<Comment>.Filter.Where(p => p.PostId == post.Id);
            IEnumerable<Comment> comments = await collection.Find(null).ToListAsync();
            return comments;
        }
        public async Task PostComment(Comment comment)
        {
            await collection.InsertOneAsync(comment);
        }
    }
}
