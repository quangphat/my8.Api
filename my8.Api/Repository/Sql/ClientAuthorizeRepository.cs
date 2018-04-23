using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Sql;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
namespace my8.Api.Repository.Sql
{
    public class ClientAuthorizeRepository:SqlRepositoryBase,IClientAuthorizeRepository
    {
        public ClientAuthorizeRepository(IOptions<SqlServerConnection> setting) : base(setting) { }

   //     public async Task<bool> Create(ClientAuthorize clientauthorize)
   //     {
			//string insert = string.Format(@"insert into ClientAuthorize (Name,ApiKey,SecretKey,Active) values (@Name,@ApiKey,@SecretKey,@Active)");

   //         try
   //         {
   //             await connection.ExecuteAsync(insert, clientauthorize);
   //             return true;
   //         }
   //         catch
   //         {
   //             return false;
   //         }
   //     }

        public async Task<ClientAuthorize> Get(string key)
        {
            string select = $"select * from ClientAuthorize  where ApiKey = '{key}'";
            IEnumerable<ClientAuthorize> clientauthorizes = await connection.QueryAsync<ClientAuthorize>(select);
            return clientauthorizes.FirstOrDefault();
        }
        //public async Task<IEnumerable<ClientAuthorize>> Search(string searchStr,int skip, int limit)
        //{
        //    IEnumerable<ClientAuthorize> clientauthorizes = await connection.QueryAsync<ClientAuthorize>("LookForClientAuthorize", new { @searchStr = searchStr, @skip = skip, @limit = limit }, commandType: System.Data.CommandType.StoredProcedure);
        //    return clientauthorizes;
        //}
        //public async Task<bool> Update(ClientAuthorize clientauthorize)
        //{
        //    string update = string.Format(@"update ClientAuthorize set Name= @Name,ApiKey= @ApiKey,SecretKey= @SecretKey,Active= @Active where ClientAuthorizeId = @ClientAuthorizeId");
        //    try
        //    {
        //        connection.Execute(update, clientauthorize);
        //        return true;
        //    }
        //    catch { return false; }
        //}
    }
}


