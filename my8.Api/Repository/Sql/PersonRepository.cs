using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using my8.Api.Interfaces.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using my8.Api.Models;

namespace my8.Api.Repository.Sql
{
    public class PersonRepository:SqlRepositoryBase,IPersonRepository
    {
        public PersonRepository(IOptions<SqlServerConnection> setting) : base(setting) { }

        public async Task<bool> Create(Person person)
        {
			string insert = string.Format(@"insert into Person (PersonId,Firstname,Lastname,Avatar,Email,EmailPrivacy,Password,Birthday,BirthdayPrivacy,WorkAs,WorkAsPrivacy,BornIn,BornInWorkAsPrivacy,Hometown,HometownWorkAsPrivacy,LiveAt,LiveAtWorkAsPrivacy,University,UniversityWorkAsPrivacy,PhoneNumber,PhoneWorkAsPrivacy,Gender,GenderWorkAsPrivacy,WorkEmail,WorkEmailWorkAsPrivacy,About,CreatedTime,ModifiedTime) values (@PersonId,@Firstname,@Lastname,@Avatar,@Email,@EmailPrivacy,@Password,@Birthday,@BirthdayPrivacy,@WorkAs,@WorkAsPrivacy,@BornIn,@BornInWorkAsPrivacy,@Hometown,@HometownWorkAsPrivacy,@LiveAt,@LiveAtWorkAsPrivacy,@University,@UniversityWorkAsPrivacy,@PhoneNumber,@PhoneWorkAsPrivacy,@Gender,@GenderWorkAsPrivacy,@WorkEmail,@WorkEmailWorkAsPrivacy,@About,@CreatedTime,@ModifiedTime)");

            try
            {
                await connection.ExecuteAsync(insert, person);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<Person> Get(string id)
        {
            string select = $"select * from Person  where PersonId = {id}";
            IEnumerable<Person> persons = await connection.QueryAsync<Person>(select);
            return persons.FirstOrDefault();
        }

        public async Task<Person> Login(string email, string password)
        {
            IEnumerable<Person> persons = await connection.QueryAsync<Person>($"select top 1 * from Person where Email = '{email}' and Password = '{password}'");
            return persons.FirstOrDefault();
        }

        public async Task<IEnumerable<Person>> Search(string searchStr,int skip, int limit)
        {
            IEnumerable<Person> persons = await connection.QueryAsync<Person>("LookForPerson", new { @searchStr = searchStr, @skip = skip, @limit = limit }, commandType: System.Data.CommandType.StoredProcedure);
            return persons;
        }
        public async Task<bool> Update(Person person)
        {
            string update = string.Format(@"update Person set Firstname= @Firstname,Lastname= @Lastname,Avatar= @Avatar,Email= @Email,EmailPrivacy= @EmailPrivacy,Password= @Password,Birthday= @Birthday,BirthdayPrivacy= @BirthdayPrivacy,WorkAs= @WorkAs,WorkAsPrivacy= @WorkAsPrivacy,BornIn= @BornIn,BornInWorkAsPrivacy= @BornInWorkAsPrivacy,Hometown= @Hometown,HometownWorkAsPrivacy= @HometownWorkAsPrivacy,LiveAt= @LiveAt,LiveAtWorkAsPrivacy= @LiveAtWorkAsPrivacy,University= @University,UniversityWorkAsPrivacy= @UniversityWorkAsPrivacy,PhoneNumber= @PhoneNumber,PhoneWorkAsPrivacy= @PhoneWorkAsPrivacy,Gender= @Gender,GenderWorkAsPrivacy= @GenderWorkAsPrivacy,WorkEmail= @WorkEmail,WorkEmailWorkAsPrivacy= @WorkEmailWorkAsPrivacy,About= @About,CreatedTime= @CreatedTime,ModifiedTime= @ModifiedTime where PersonId = @PersonId");
            try
            {
                await connection.ExecuteAsync(update, person);
                return true;
            }
            catch { return false; }
        }
    }
}


