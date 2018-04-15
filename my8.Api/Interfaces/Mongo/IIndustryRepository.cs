using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IIndustryRepository
    {
        Task<string> Create(Industry industry);
        Task<Industry> Get(string id);
        Task<Industry> GetbyCode(string code);
        Task<List<Industry>> Search(string searchStr);
        Task<List<Industry>> Gets();
        Task<bool> Update(Industry industry);
        Task<bool> Delete(string id);
        //Task<List<Industry>> GetsByPerson(string personId);
    }
}

