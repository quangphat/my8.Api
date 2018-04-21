using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using my8.Api.Infrastructures;
using MongoI = my8.Api.Interfaces.Mongo;
using NeoI = my8.Api.Interfaces.Neo4j;
using SqlI = my8.Api.Interfaces.Sql;
using MongoR = my8.Api.Repository.Mongo;
using NeoR = my8.Api.Repository.Neo4j;
using SqlR = my8.Api.Repository.Sql;
using Newtonsoft.Json.Serialization;
using my8.Api.IBusiness;
using my8.Api.Business;
using my8.Api.SmartCenter;
using Microsoft.AspNetCore.SignalR;
namespace my8.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:60202");
            }));

            services.AddSignalR();
            services.Configure<MongoConnection>(Configuration.GetSection("MongoConnection"));
            services.Configure<Neo4jConnection>(Configuration.GetSection("Neo4jConnection"));
            services.Configure<SqlServerConnection>(Configuration.GetSection("SqlServerConnection"));
            //services.AddMvc();
            services.AddMvc().AddJsonOptions(opts =>
            {
                opts.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });
            MapConfig.Config(services);
            services.AddScoped<NotificationHub>();
            //SmartCenter
            services.AddScoped<ISmartCenter, SmartCenter.SmartCenter>();
            //Business
            services.AddScoped<IPageBusiness, PageBusiness>();
			services.AddScoped<IPersonBusiness, PersonBusiness>();
			services.AddScoped<IClubBusiness, ClubBusiness>();
			services.AddScoped<IStatusPostBusiness, StatusPostBusiness>();
			services.AddScoped<IPostBroadcastPersonBusiness, PostBroadcastPersonBusiness>();
			services.AddScoped<IIndustryBusiness, IndustryBusiness>();
			services.AddScoped<ISkillBusiness, SkillBusiness>();
			services.AddScoped<ILocationBusiness, LocationBusiness>();
			services.AddScoped<IProvinceBusiness, ProvinceBusiness>();
			services.AddScoped<IDistrictBusiness, DistrictBusiness>();
			services.AddScoped<IDegreeBusiness, DegreeBusiness>();
			services.AddScoped<ISeniorityLevelBusiness, SeniorityLevelBusiness>();
			services.AddScoped<IJobPostBusiness, JobPostBusiness>();
			services.AddScoped<ICommentBusiness, CommentBusiness>();
			services.AddScoped<IReplyCommentBusiness, ReplyCommentBusiness>();
			//<AppendBusinessDI>

            //Mongo
            services.AddSingleton<MongoI.IStatusPostRepository, MongoR.StatusPostRepository>();
            services.AddSingleton<MongoI.ICommentRepository, MongoR.CommentRepository>();
            services.AddSingleton<MongoI.IJobPostRepository, MongoR.JobPostRepository>();
            services.AddSingleton<MongoI.IPersonRepository, MongoR.PersonRepository>();
		    services.AddSingleton<MongoI.IJobPostRepository, MongoR.JobPostRepository>();
			services.AddSingleton<MongoI.IJobPostRepository, MongoR.JobPostRepository>();
			services.AddSingleton<MongoI.IUniversityRepository, MongoR.UniversityRepository>();
			services.AddSingleton<MongoI.IDeletedPostRepository, MongoR.DeletedPostRepository>();
			services.AddSingleton<MongoI.IClubRepository, MongoR.ClubRepository>();
			services.AddSingleton<MongoI.IActorTypeRepository, MongoR.ActorTypeRepository>();
            services.AddSingleton<MongoI.IPageRepository, MongoR.PageRepository>();
			services.AddSingleton<MongoI.IPostBroadcastPersonRepository, MongoR.PostBroadcastPersonRepository>();
			services.AddSingleton<MongoI.IIndustryRepository, MongoR.IndustryRepository>();
			services.AddSingleton<MongoI.ISkillRepository, MongoR.SkillRepository>();
			services.AddSingleton<MongoI.ILocationRepository, MongoR.LocationRepository>();
			services.AddSingleton<MongoI.IProvinceRepository, MongoR.ProvinceRepository>();
			services.AddSingleton<MongoI.IDistrictRepository, MongoR.DistrictRepository>();
			services.AddSingleton<MongoI.IDegreeRepository, MongoR.DegreeRepository>();
			services.AddSingleton<MongoI.ISeniorityLevelRepository, MongoR.SeniorityLevelRepository>();
			services.AddSingleton<MongoI.IReplyCommentRepository, MongoR.ReplyCommentRepository>();
			//AppendMongoDI
            //Neo
            services.AddSingleton<NeoI.IPageRepository, NeoR.PageRepository>();
            services.AddSingleton<NeoI.IPersonRepository, NeoR.PersonRepository>();
			services.AddSingleton<NeoI.IClubRepository, NeoR.ClubRepository>();
			//AppendNeoDI
            //Sql
            services.AddSingleton<SqlI.IPersonRepository, SqlR.PersonRepository>();
			//AppendSqlDI
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //app.UseMiddleware<HandShakeAuthorizeMiddleware>();
            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("chat");
                routes.MapHub<NotificationHub>("notification");
            });
            app.UseMvc();
            
        }
    }
}
