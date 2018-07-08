using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IJobFunctionBusiness
    {
        Task<JobFunction> Create(JobFunction JobFunction);
        Task<JobFunction> Get(string JobFunctionId);
        Task<JobFunction> GetByCode(string code);
        Task<List<JobFunction>> Gets();
        Task<bool> Update(JobFunction JobFunction);
        Task<bool> Delete(string id);
        Task<List<JobFunction>> Search(string searchStr);
    }
}
