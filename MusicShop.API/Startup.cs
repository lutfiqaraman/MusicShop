using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MusicShop.Core;
using MusicShop.Core.Repositories;
using MusicShop.Core.Services;
using MusicShop.Data.MongoDB.Repository;
using MusicShop.Data.MongoDB.Setting;
using MusicShop.Data.SQL;
using MusicShop.Services.Services;

namespace MusicShop.API
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
            services.AddControllers();

            //START :: Configuration for SQL Server
            services.AddDbContext<MusicStoreDbContext>(
                options => 
                options.UseSqlServer(
                    Configuration.GetConnectionString("Default"), x => x.MigrationsAssembly("MusicShop.Data.SQL")
                ));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IMusicService, MusicService>();
            services.AddTransient<IArtistService, ArtistService>();
            //END :: Configuration for SQL Server

            //START :: Configuration of MongoDB
            services.Configure<Settings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetValue<string>("MongoDB:ConnectionString");
                    options.Database = Configuration.GetValue<string>("MongoDB:Database");
                });

            services.AddSingleton<IMongoClient, MongoClient>(
                options => new MongoClient(Configuration.GetValue<string>("MongoDB:ConnectionString")));
            
            services.AddTransient<IDatabaseSettings, DatabaseSettings>();
            services.AddScoped<IComposerRepository, ComposerRepository>();
            services.AddTransient<IComposerService, ComposerService>();
            //END :: Configuration of MongoDB

            //Swagger
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Title = "Swagger - MusicShop",
                        Description = "Swagger file is used for MusicShop API"
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.RoutePrefix = "";
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Music Shop V1");
            });
        }
    }
}
