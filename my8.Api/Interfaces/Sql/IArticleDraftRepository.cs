using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace my8.Api.Interfaces.Sql
{
    public interface IArticleDraftRepository
    {
        Task<bool> Create(ArticleDraft articledraft);
        Task<ArticleDraft> Get(string id);
        Task<bool> Update(ArticleDraft articledraft);
    }
}

