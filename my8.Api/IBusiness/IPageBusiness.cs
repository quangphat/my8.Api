using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.IBusiness
{
    public interface IPageBusiness
    {
        Task<Page> Create(Page page);
        Task<Page> Get(string id);
        Task<List<PersonAllin>> GetPeopleFollow(string pageId);
        Task<List<PageAllin>> Search(string searchStr, int skip, int limit);
    }
}
