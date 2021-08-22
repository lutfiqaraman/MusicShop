using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MusicShop.Core;
using MusicShop.Core.Repositories;
using MusicShop.Data.MongoDB.Repository;
using MusicShop.Data.MongoDB.Setting;
using MusicShop.Data.SQL;

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
            //END :: Configuration for SQL Server

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Configuration of MongoDB
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
        }
    }
}
