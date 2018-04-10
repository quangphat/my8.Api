using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Sql;
using my8.Api.Models.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace my8.Api.Repository.Sql
{
    public class PersonRepository:SqlRepositoryBase,IPersonRepository
    {
        public PersonRepository(IOptions<SqlServerConnection> setting) : base(setting) { }

        public async Task<bool> Create(Person person)
        {
            throw new NotImplementedException();
        }

        public async Task<Person> Get(string id)
        {
            throw new NotImplementedException();
        }
    }
}

