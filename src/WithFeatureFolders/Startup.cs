using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WithFeatureFolders.Core.Interfaces;
using WithFeatureFolders.Core.Model;
using WithFeatureFolders.Infrastructure.Data;

namespace WithFeatureFolders
{
    public class Startup
    {

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase());

            // Add framework services.
            services.AddMvc(o => o.Conventions.Add(new FeatureConvention()))
                .AddRazorOptions(options =>
                {
                    // {0} - Action Name
                    // {1} - Controller Name
                    // {2} - Area Name
                    // {3} - Feature Name
                    options.AreaViewLocationFormats.Clear();
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Features/{3}/{1}/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Features/{3}/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/{2}/Features/Shared/{0}.cshtml");
                    options.AreaViewLocationFormats.Add("/Areas/Shared/{0}.cshtml");

                    // replace normal view location entirely
                    options.ViewLocationFormats.Clear();
                    options.ViewLocationFormats.Add("/Features/{3}/{1}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/{3}/{0}.cshtml");
                    options.ViewLocationFormats.Add("/Features/Shared/{0}.cshtml");

                    // add support for features side-by-side with /Views
                    // (do NOT clear ViewLocationFormats)
                    //options.ViewLocationFormats.Insert(0, "/Features/Shared/{0}.cshtml");
                    //options.ViewLocationFormats.Insert(0, "/Features/{3}/{0}.cshtml");
                    //options.ViewLocationFormats.Insert(0, "/Features/{3}/{1}/{0}.cshtml");
                    //
                    // (do NOT clear AreaViewLocationFormats)
                    //options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Features/Shared/{0}.cshtml");
                    //options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Features/{3}/{0}.cshtml");
                    //options.AreaViewLocationFormats.Insert(0, "/Areas/{2}/Features/{3}/{1}/{0}.cshtml");


                    options.ViewLocationExpanders.Add(new FeatureViewLocationExpander());
                });

            services.AddScoped<IRepository<Ninja>, EfRepository<Ninja>>();
            services.AddScoped<IRepository<Pirate>, EfRepository<Pirate>>();
            services.AddScoped<IRepository<Plant>, EfRepository<Plant>>();
            services.AddScoped<IRepository<Zombie>, EfRepository<Zombie>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
