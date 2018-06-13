using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using my8.Api.Models;
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
            mapper.CreateMap<StatusPost, Feed>();
            mapper.CreateMap<Feed, JobPost>();
            mapper.CreateMap<JobPost, Feed>();
            mapper.CreateMap<PostBroadcastPerson, Feed>()
                .ForMember(a => a.Liked, b => b.MapFrom(c => c.Liked))
                .ForMember(a => a.BroadcastId, b => b.MapFrom(c => c.Id));
            mapper.CreateMap<PersonAllin, Receiver>()
                .ForMember(a => a.PersonId, b => b.MapFrom(c => c.Person.Id))
                .ForMember(a => a.Like, b=>b.UseValue(false));
			//<AppendNewHere>
        }
    }
}
