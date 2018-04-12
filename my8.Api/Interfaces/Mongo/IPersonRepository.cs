
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IPersonRepository
    {
        Task<string> Create(Person Person);
        Task<bool> Update(Person person);
        Task<Person> Get(string id);
    }
}
