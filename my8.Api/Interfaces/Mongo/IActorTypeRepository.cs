using my8.Api.Models.Mongo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IActorTypeRepository
    {
        Task<bool> Create(ActorType actortype);
        Task<ActorType> Get(string id);
    }
}

