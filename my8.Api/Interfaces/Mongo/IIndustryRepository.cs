using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IJobFunctionRepository
    {
        Task<string> Create(JobFunction JobFunction);
        Task<JobFunction> Get(string id);
        Task<JobFunction> GetbyCode(string code);
        Task<List<JobFunction>> Search(string searchStr);
        Task<List<JobFunction>> Gets();
        Task<bool> Update(JobFunction JobFunction);
        Task<bool> Delete(string id);
        //Task<List<JobFunction>> GetsByPerson(string personId);
    }
}

