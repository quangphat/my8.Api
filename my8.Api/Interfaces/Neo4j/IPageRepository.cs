using my8.Api.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace my8.Api.Interfaces.Neo4j
{
    public interface IPageRepository
    {
        Task<bool> Create(Page page);
        Task<bool> Update(Page page);
        Task<PageAllin> Get(string id);
        Task<IEnumerable<Page>> Search(string searchStr,int limit);
        Task<IEnumerable<PageAllin>> Search(string searchStr,int skip, int limit);
        Task<IEnumerable<PersonAllin>> GetPersonFollow(string pageId);
    }
}
