using my8.Api.IBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoI = my8.Api.Interfaces.Mongo;
using my8.Api.Models;
using my8.Api.Infrastructures;

namespace my8.Api.Business
{
    public class ExperienceBusiness :BaseBusiness, IExperienceBusiness
    {
        MongoI.IExperienceRepository m_ExperienceRepositoryM;
        public ExperienceBusiness(MongoI.IExperienceRepository experienceRepoM, CurrentProcess process):base(process)
        {
            m_ExperienceRepositoryM = experienceRepoM;
        }
        public async Task<Experience> Create(Experience experience)
        {
            if(experience.FromDate==null)
            {
                return null;
            }
            experience.CreatedTime = experience.UpdatedTime = Utils.GetUnixTime();
            experience.FromDateUnix = Utils.GetUnixTime(experience.FromDate);
            if (experience.ToDate == null)
                experience.isCurrentlyWorkHere = true;
            else if (experience.isCurrentlyWorkHere)
            {
                experience.ToDate = null;
            }

            string id = await m_ExperienceRepositoryM.Create(experience);
            return experience;
        }

        public async Task<Experience> Get(string experienceId)
        {
            return await m_ExperienceRepositoryM.Get(experienceId);
        }
        public async Task<Pagination<Experience>> GetByPersonId(string profileId, int page, int limit = 10)
        {
            if (CheckIsNotLogin())
            {
                return null;
            }
            //miss check privacy here
            return await m_ExperienceRepositoryM.GetByPersonId(profileId,page,limit);
        }
        public async Task<bool> Update(Experience experience)
        {

            return await m_ExperienceRepositoryM.Update(experience);
        }
        public async Task<bool> Delete(string id)
        {
            return await m_ExperienceRepositoryM.Delete(id);
        }
        public async Task<List<Experience>> Search(string searchStr)
        {
            searchStr = searchStr.ToLower();
            return await m_ExperienceRepositoryM.Search(searchStr);
        }
    }
}
