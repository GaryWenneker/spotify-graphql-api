using GraphiQl;
using GraphQL;
using GraphQL.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using musiXmatch.Interfaces;
using musiXmatch.Models;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials;
using FluentSpotifyApi.AuthorizationFlows.ClientCredentials.Extensions;
using FluentSpotifyApi.Extensions;

namespace musiXmatch
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            services.AddApiVersioning();

            services.AddCors(o => o.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services
                .Configure<ClientCredentialsFlowOptions>(
                    this.Configuration.GetSection("ClientCredentialsFlowOptions"));

            services
                .AddFluentSpotifyClient(clientBuilder => clientBuilder
                    .ConfigurePipeline(pipeline => pipeline.AddClientCredentialsFlow()));



            services.AddTransient<IRepository, Repository>();
            services.AddSingleton<IDocumentExecuter, DocumentExecuter>();
            services.AddSingleton<Query>();

            services.AddSingleton<Models.GraphTypes.Image>();
            services.AddSingleton<Models.GraphTypes.Followers>();       
            services.AddSingleton<Models.GraphTypes.Artist>();
            services.AddSingleton<Models.GraphTypes.SimpleAlbum>();
            services.AddSingleton<Models.GraphTypes.SimpleTrack>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new Models.Schema(new FuncDependencyResolver(type => sp.GetService(type))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var corsPolicy = Configuration["AppConfiguration:CorsPolicy"];
            app.UseCors(corsPolicy);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseGraphiQl("/graphql", "/v1/graphql");
            }
            else
            {
                app.UseHsts();
            }


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
