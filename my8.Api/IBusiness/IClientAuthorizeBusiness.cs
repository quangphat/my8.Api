using my8.Api.Infrastructures;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IClientAuthorizeBusiness
    {
        //Task<ClientAuthorize> Create(ClientAuthorize clientauthorize);
        Task<ClientAuthorize> Get(string key);
        //Task<bool> Update(ClientAuthorize clientauthorize);
        //Task<bool> Delete(string id);
        //Task<List<ClientAuthorize>> Search(string searchStr);
    }
}
