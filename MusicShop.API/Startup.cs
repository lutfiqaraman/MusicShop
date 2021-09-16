using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using MusicShop.Core;
using MusicShop.Core.Repositories;
using MusicShop.Core.Services;
using MusicShop.Data.MongoDB.Repository;
using MusicShop.Data.MongoDB.Setting;
using MusicShop.Data.SQL;
using MusicShop.Services.Services;
using System.Text;
using System.Threading.Tasks;

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
            services.AddTransient<IUserService, UserService>();

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

            //Automapper
            services.AddAutoMapper(typeof(Startup));

            //Authentication
            byte[] key = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("AppSettings:Secret"));

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x => {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userService = 
                                context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                            var userId = int.Parse(context.Principal.Identity.Name);
                            var user = userService.GetUserById(userId);

                            if (user == null)
                                context.Fail("Unauthorized");

                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
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
