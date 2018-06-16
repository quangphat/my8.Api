using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using my8.Api.Models;
using my8.Api.my8Enum;
using System.Collections;
using ModelM = my8.Api.Models;

namespace my8.Api.Infrastructures
{
    public class MapConfig
    {
        public static void Config(IServiceCollection services)
        {
            Mapper.Initialize(mapper =>
            {
                ClientProfile.Config(services, mapper);
                ConfigMapper(mapper);
            });
        }
        public static void ConfigMapper(IMapperConfigurationExpression mapper)
        {
            mapper.AllowNullCollections = true;
            mapper.CreateMap<ObjectId, string>().ConvertUsing(a => a.ToString());
            mapper.CreateMap<string, ObjectId>().ConvertUsing(a => ObjectId.Parse(a));
            mapper.CreateMap<PostBroadcastPerson, PostBroadcastPersonHidden>();
            mapper.CreateMap<PostBroadcastPersonHidden, PostBroadcastPerson>();
            mapper.CreateMap<Feed, StatusPost>();
            mapper.CreateMap<StatusPost, Feed>()
                .ForMember(p => p.PostType, b => b.UseValue((int)PostType.StatusPost));
            mapper.CreateMap<Feed, JobPost>();
            mapper.CreateMap<JobPost, Feed>()
                .ForMember(p => p.PostType, b => b.UseValue((int)PostType.JobPost));
            mapper.CreateMap<PostBroadcastPerson, Feed>()
                .ForMember(a => a.Liked, b => b.MapFrom(c => c.Like))
                .ForMember(a => a.BroadcastId, b => b.MapFrom(c => c.Id));
            mapper.CreateMap<PersonAllin, Receiver>()
                .ForMember(a => a.PersonId, b => b.MapFrom(c => c.Person.Id))
                .ForMember(a => a.Like, b=>b.UseValue(false));
            mapper.CreateMap<Page, Author>()
                .ForMember(a => a.AuthorId, (IMemberConfigurationExpression<Page, Author, string> b) => b.MapFrom(c => c.PageId))
                .ForMember(a => a.AuthorTypeId, (IMemberConfigurationExpression<Page, Author, int> b) => b.UseValue((int)my8Enum.AuthorType.Page));
            mapper.CreateMap<Community, Author>()
               .ForMember(a => a.AuthorId, (IMemberConfigurationExpression<Community, Author, string> b) => b.MapFrom(c => c.CommunityId))
               .ForMember(a => a.AuthorTypeId, (IMemberConfigurationExpression<Community, Author, int> b) => b.UseValue((int)my8Enum.AuthorType.Community));
            //<AppendNewHere>
        }
    }
}
