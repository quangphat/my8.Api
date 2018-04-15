using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface ISeniorityLevelRepository
    {
        Task<string> Create(SeniorityLevel senioritylevel);
        Task<SeniorityLevel> Get(string id);
        Task<List<SeniorityLevel>> Gets();
        Task<bool> Update(SeniorityLevel senioritylevel);
        Task<bool> Delete(string id);
        Task<List<SeniorityLevel>> Search(string searchStr);
    }
}

