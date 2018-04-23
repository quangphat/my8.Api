using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using my8.Api.Models;
using AutoMapper;
using my8.Api.Interfaces.Sql;

namespace my8.Api.Business
{
    public class ClientAuthorizeBusiness : IClientAuthorizeBusiness
    {
        IClientAuthorizeRepository m_ClientAuthorizeRepositoryM;
        public ClientAuthorizeBusiness(IClientAuthorizeRepository clientauthorizeRepoM)
        {
            m_ClientAuthorizeRepositoryM = clientauthorizeRepoM;
        }
        //public async Task<ClientAuthorize> Create(ClientAuthorize clientauthorize)
        //{
        //    string id = await m_ClientAuthorizeRepositoryM.Create(clientauthorize);
        //    clientauthorize.Id = id;
        //    return clientauthorize;
        //}

        public async Task<ClientAuthorize> Get(string key)
        {
            return await m_ClientAuthorizeRepositoryM.Get(key);
        }
        //public async Task<bool> Update(ClientAuthorize clientauthorize)
        //{
        //    return await m_ClientAuthorizeRepositoryM.Update(clientauthorize);
        //}
        //public async Task<bool> Delete(string id)
        //{
        //    return await m_ClientAuthorizeRepositoryM.Delete(id);
        //}
        //public async Task<List<ClientAuthorize>> Search(string searchStr)
        //{
        //    searchStr = searchStr.ToLower();
        //    return await m_ClientAuthorizeRepositoryM.Search(searchStr);
        //}
    }
}
