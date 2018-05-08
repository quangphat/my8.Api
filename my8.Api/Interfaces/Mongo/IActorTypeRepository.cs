using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IAuthorTypeRepository
    {
        Task<bool> Create(AuthorType authortype);
        Task<AuthorType> Get(string id);
    }
}

