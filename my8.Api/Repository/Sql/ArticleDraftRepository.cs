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
    public class ArticleDraftRepository:SqlRepositoryBase,IArticleDraftRepository
    {
        public ArticleDraftRepository(IOptions<SqlServerConnection> setting) : base(setting) { }

        public async Task<bool> Create(ArticleDraft articledraft)
        {
			string insert = string.Format(@"insert into ArticleDraft () values ()");

            try
            {
                await connection.ExecuteAsync(insert, articledraft);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ArticleDraft> Get(string id)
        {
            string select = $"select * from ArticleDraft  where ArticleDraftId = {id}";
            IEnumerable<ArticleDraft> articledrafts = await connection.QueryAsync<ArticleDraft>(select);
            return articledrafts.FirstOrDefault();
        }
        public async Task<IEnumerable<ArticleDraft>> Search(string searchStr,int skip, int limit)
        {
            IEnumerable<ArticleDraft> articledrafts = await connection.QueryAsync<ArticleDraft>("LookForArticleDraft", new { @searchStr = searchStr, @skip = skip, @limit = limit }, commandType: System.Data.CommandType.StoredProcedure);
            return articledrafts;
        }
        public async Task<bool> Update(ArticleDraft articledraft)
        {
            string update = string.Format(@"update ArticleDraft set  where ArticleDraftId = @ArticleDraftId");
            try
            {
                connection.Execute(update, articledraft);
                return true;
            }
            catch { return false; }
        }
    }
}


