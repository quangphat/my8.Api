using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface ISeniorityLevelBusiness
    {
        Task<SeniorityLevel> Create(SeniorityLevel senioritylevel);
        Task<SeniorityLevel> Get(string senioritylevelId);
        Task<List<SeniorityLevel>> Gets();
        Task<bool> Update(SeniorityLevel senioritylevel);
        Task<bool> Delete(string id);
        Task<List<SeniorityLevel>> Search(string searchStr);
    }
}
