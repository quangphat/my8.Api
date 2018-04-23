using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace my8.Api.Interfaces.Sql
{
    public interface IClientAuthorizeRepository
    {
        //Task<bool> Create(ClientAuthorize clientauthorize);
        Task<ClientAuthorize> Get(string key);
        //Task<bool> Update(ClientAuthorize clientauthorize);
    }
}

