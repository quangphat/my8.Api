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
                opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });
            MapConfig.Config(services);

            //Business
            services.AddScoped<IPageBusiness, PageBusiness>();
			services.AddScoped<IPersonBusiness, PersonBusiness>();
			services.AddScoped<ITeamBusiness, TeamBusiness>();
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
            services.AddSingleton<MongoI.ITeamRepository, MongoR.TeamRepository>();
			//AppendMongoDI
            //Neo
            services.AddSingleton<NeoI.IPageRepository, NeoR.PageRepository>();
            services.AddSingleton<NeoI.IPersonRepository, NeoR.PersonRepository>();
			services.AddSingleton<NeoI.ITeamRepository, NeoR.TeamRepository>();
			//AppendNeoDI
            //Sql
            services.AddSingleton<SqlI.IPersonRepository, SqlR.PersonRepository>();
			services.AddSingleton<SqlI.ITeamRepository, SqlR.TeamRepository>();
			//AppendSqlDI
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware<HandShakeAuthorizeMiddleware>();
            app.UseCors("CorsPolicy");

            app.UseSignalR(routes =>
            {
                routes.MapHub<ChatHub>("chat");
            });
            app.UseMvc();
            
        }
    }
}
