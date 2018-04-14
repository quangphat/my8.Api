using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IPageRepository
    {
        Task<string> Create(Page page);
        Task<bool> Update(Page page);
        Task<Page> Get(string id);
    }
}

