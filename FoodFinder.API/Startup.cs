using FoodFinder.API.Authentication;
using FoodFinder.API.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace FoodFinder.API
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        public Startup(IHostingEnvironment env)
        {
            _environment = env;

            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Setup Database
            services.AddDbContext<FoodFinderContext>(options =>
            {
                if (_environment.IsDevelopment())
                    options.UseSqlite(Configuration.GetConnectionString("SqliteConnectionString"));
                else
                    options.UseSqlServer(Configuration.GetConnectionString("MS_TableConnectionString"));
            });


            // only allow HTTPS for production
            services.Configure<MvcOptions>(options =>
            {
                if (_environment.IsProduction())
                    options.Filters.Add(new RequireHttpsAttribute());
            });

            // Add framework services.
            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
            });

            // Add applicationkey security
            services.AddScoped<ApplicationKeyAttributem>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

        }
    }
}