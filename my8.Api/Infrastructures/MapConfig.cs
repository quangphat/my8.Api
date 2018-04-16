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
            mapper.CreateMap<PostAllType, StatusPost>();
            mapper.CreateMap<StatusPost, PostAllType>();
            mapper.CreateMap<PostAllType, JobPost>();
            mapper.CreateMap<JobPost, PostAllType>();
			//<AppendNewHere>
        }
    }
}
