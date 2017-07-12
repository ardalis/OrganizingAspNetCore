using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WithRazorPages.Core.Model;
using WithRazorPages.Core.Interfaces;
using WithRazorPages.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace WithRazorPages
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
            services.AddDbContext<ApplicationDbContext>(
                optionsBuilder => optionsBuilder.UseInMemoryDatabase("db"));

            services.AddMvc();
            //    .AddRazorOptions(options =>
            //{
            //    options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
            //});

            services.AddScoped<IRepository<Ninja>, EfRepository<Ninja>>();
            services.AddScoped<IRepository<Pirate>, EfRepository<Pirate>>();
            services.AddScoped<IRepository<Plant>, EfRepository<Plant>>();
            services.AddScoped<IRepository<Zombie>, EfRepository<Zombie>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
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
