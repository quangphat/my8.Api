using my8.Api.IBusiness;
using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.SmartCenter
{
    public class SmartCenter:ISmartCenter
    {
        private const int MAX_LIMIT = 10;
        IStatusPostBusiness m_StatusPostBusiness;
        IJobPostBusiness m_JobPostBusiness;
        IPostBroadcastPersonBusiness m_PostBroadcastPersonBusiness;
        public SmartCenter(IStatusPostBusiness statuspostBusiness,IJobPostBusiness jobPostBusiness,IPostBroadcastPersonBusiness postBroadcastPersonBusiness)
        {
            m_StatusPostBusiness = statuspostBusiness;
            m_JobPostBusiness = jobPostBusiness;
            m_PostBroadcastPersonBusiness = postBroadcastPersonBusiness;
        }

        public async Task<List<PostAllType>> Gets(string personId,int skip)
        {
            List<PostBroadcastPerson> postBroadcasts = await m_PostBroadcastPersonBusiness.GetByPerson(personId, skip, MAX_LIMIT);
        }
    }
}
