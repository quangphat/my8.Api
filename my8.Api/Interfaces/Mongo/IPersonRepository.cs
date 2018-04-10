
using my8.Api.Models.Mongo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IPersonRepository
    {
        Task<bool> Create(Person Person);
        Task<bool> Update(Person person);
        Task<Person> Get(string id);
    }
}
