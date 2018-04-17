using AutoMapper;
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
        private const int MAX_LIMIT = 100;
        IStatusPostBusiness m_StatusPostBusiness;
        IJobPostBusiness m_JobPostBusiness;
        IPostBroadcastPersonBusiness m_PostBroadcastPersonBusiness;
        string[] postBroadcastJobsIds = null;
        string[] postBroadcastStatusIds = null;
        public SmartCenter(IStatusPostBusiness statusPostBusiness,IJobPostBusiness jobPostBusiness,IPostBroadcastPersonBusiness postBroadcastPersonBusiness)
        {
            m_StatusPostBusiness = statusPostBusiness;
            m_JobPostBusiness = jobPostBusiness;
            m_PostBroadcastPersonBusiness = postBroadcastPersonBusiness;
        }

        public async Task<List<PostAllType>> Gets(string personId,int skip)
        {
            
            List<PostAllType> lstPostJob = null;
            List<PostAllType> lstPostStatus = null;
            List<PostBroadcastPerson> postBroadcasts = await m_PostBroadcastPersonBusiness.GetByPerson(personId, skip, MAX_LIMIT);
            List<PostBroadcastPerson> postBroadcastJobs = postBroadcasts.Where(p => p.PostType == my8Enum.PostTypeEnum.JobPost).ToList();
            List<PostBroadcastPerson> postBroadcastStatus = postBroadcasts.Where(p => p.PostType == my8Enum.PostTypeEnum.StatusPost).ToList();
            Task tt1 = GetJobPostIdArray(postBroadcastJobs);
            Task tt2 = GetStatusPostIdArray(postBroadcastStatus);
            await Task.WhenAll(tt1, tt2);
            Task<List<JobPost>> t1 = m_JobPostBusiness.Gets(postBroadcastJobsIds);
            Task<List<StatusPost>> t2 = m_StatusPostBusiness.Gets(postBroadcastStatusIds);
            await Task.WhenAll(t1, t2);
            lstPostJob = Mapper.Map<List<PostAllType>>(t1.Result);
            lstPostStatus = Mapper.Map<List<PostAllType>>(t2.Result);
            if (lstPostJob == null) lstPostJob = new List<PostAllType>();
            if (lstPostStatus == null) lstPostStatus = new List<PostAllType>();
            List<PostAllType> lstAll = lstPostStatus.Concat(lstPostJob).ToList();
            postBroadcastJobsIds = null;
            postBroadcastStatusIds = null;
            return lstAll;
        }
        private async Task GetJobPostIdArray(List<PostBroadcastPerson> lstJobPostBroadCast)
        {
            if(lstJobPostBroadCast==null)
            {
                postBroadcastJobsIds = new string[] { };
            }
            await Task.Run(()=> {
                postBroadcastJobsIds = lstJobPostBroadCast.Select(p => p.PostId).ToArray();
                if (postBroadcastJobsIds == null)
                {
                    postBroadcastJobsIds = new string[] { };
                }
            });
        }
        private async Task GetStatusPostIdArray(List<PostBroadcastPerson> lstStatusPostBroadCast)
        {
            if (lstStatusPostBroadCast == null)
            {
                postBroadcastStatusIds = new string[] { };
            }
            await Task.Run(() => {
                postBroadcastStatusIds = lstStatusPostBroadCast.Select(p => p.PostId).ToArray();
                if (postBroadcastJobsIds == null)
                {
                    postBroadcastStatusIds = new string[] { };
                }
            });
        }
    }
}
