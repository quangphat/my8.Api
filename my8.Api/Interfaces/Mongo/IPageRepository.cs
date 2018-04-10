using my8.Api.Models.Mongo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IPageRepository
    {
        Task<bool> Create(Page page);
        Task<Page> Get(string id);
    }
}

