using my8.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace my8.Api.IBusiness
{
    public interface IDegreeBusiness
    {
        Task<Degree> Create(Degree degree);
        Task<Degree> Get(string degreeId);
        Task<List<Degree>> Gets();
        Task<bool> Update(Degree degree);
        Task<bool> Delete(string id);
        Task<List<Degree>> Search(string searchStr);
    }
}
