using my8.Api.Models.Sql;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Sql
{
    public interface IPersonRepository
    {
        Task<bool> Create(Person Person);
        Task<Person> Get(string id);
    }
}
