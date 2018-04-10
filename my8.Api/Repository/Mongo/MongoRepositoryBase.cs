using Microsoft.Extensions.Options;
using MongoDB.Driver;
using my8.Api.Infrastructures;

namespace my8.Api.Repository.Mongo
{
    public abstract class MongoRepositoryBase
    {
        protected MongoClient _client;
        protected IMongoDatabase _db { get; set; }
        MongoConnection mongoConnection;
        public MongoRepositoryBase(IOptions<MongoConnection> setting)
        {
            mongoConnection = setting.Value;
            Connect(mongoConnection);
        }
        private void Connect(MongoConnection mongoConnection)
        {
            _client = new MongoClient($"mongodb://{mongoConnection.UserName}:{mongoConnection.Password}@{mongoConnection.ServerURl}");
            if (_db == null)
                _db= _client.GetDatabase(mongoConnection.Database);
        }
    }
}
