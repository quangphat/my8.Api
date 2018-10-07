using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using AutoMapper;
using my8.Api.Infrastructures;

namespace my8.Api.Business
{
    public class JobFunctionBusiness :BaseBusiness, IJobFunctionBusiness
    {
        MongoI.IJobFunctionRepository m_JobFunctionRepositoryM;
        public JobFunctionBusiness(MongoI.IJobFunctionRepository JobFunctionRepoM,CurrentProcess process):base(process)
        {
            m_JobFunctionRepositoryM = JobFunctionRepoM;
        }
        public async Task<JobFunction> Create(JobFunction JobFunction)
        {
            JobFunction.Code = JobFunction.Code.FormatCode();
            string id = await m_JobFunctionRepositoryM.Create(JobFunction);
            JobFunction.Id = id;
            return JobFunction;
        }

        public async Task<JobFunction> Get(string JobFunctionId)
        {
            return await m_JobFunctionRepositoryM.Get(JobFunctionId);
        }

        public async Task<JobFunction> GetByCode(string code)
        {
            return await m_JobFunctionRepositoryM.GetbyCode(code);
        }

        public async Task<List<JobFunction>> Gets()
        {
            return await m_JobFunctionRepositoryM.Gets();
        }

        public async Task<bool> Update(JobFunction JobFunction)
        {
            return await m_JobFunctionRepositoryM.Update(JobFunction);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_JobFunctionRepositoryM.Delete(id);
        }

        public async Task<List<JobFunction>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_JobFunctionRepositoryM.Search(searchStr);
        }
    }
}
