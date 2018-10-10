
using my8.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace my8.Api.Interfaces.Mongo
{
    public interface IPersonRepository
    {
        Task<string> Create(Person Person);
        Task<bool> Update(Person person);
        Task<Person> Get(string id);
        Task<List<Person>> SearchByIndustries(string[] keySearchs);
        Task<List<Person>> SearchBySkills(string[] keySearchs);
        Task<List<Person>> SearchByLocations(string[] keySearchs);
        Task<List<Person>> SearchByDegrees(string[] keySearchs);
        Task<List<Person>> SearchByExperience(int min,int max);
        Task<Person> GetByProfileName(string url);
        //Task<List<Person>> SearchBySeniorities(string[] keySearchs);
        //Task<List<Author>> SearchByEmploymentType(string keySearchs);
    }
}
