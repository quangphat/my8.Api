
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Sql
{
    public interface IPersonRepository
    {
        Task<bool> Create(Person person);
        Task<Person> Get(string id);
        Task<bool> Update(Person person);
        Task<IEnumerable<Person>> Search(string searchStr, int skip, int limit);
    }
}

