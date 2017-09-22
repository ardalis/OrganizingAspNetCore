using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WithRazorPages.Core.Model;
using WithRazorPages.Core.Interfaces;
using WithRazorPages.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

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

            services.AddMvc()
                .AddRazorOptions(options =>
                {
                    //options.PageViewLocationFormats.Add("/Pages/Shared/{0}.cshtml");
                })
                .AddRazorPagesOptions(rpopt =>
                {
                    rpopt.Conventions.AddPageRoute("/Pages/Shared/Error", "/Error");
                    rpopt.Conventions.ConfigureFilter(new SelectionLoggingFilter());
                });

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

    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }

    public class HandlerChangingPageFilterAttribute : Attribute, IPageFilter
    {
        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            context.HandlerMethod = context.ActionDescriptor.HandlerMethods.First(m => m.Name == "Edit");
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }
    }

    public class SelectionLoggingFilter : IPageFilter
    {
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
            var factory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
            var logger = factory.CreateLogger(context.HandlerInstance.ToString());
            var instance = context.HandlerInstance as PageModel;
            if (instance != null)
            {
                logger.LogWarning(instance.PageContext.ActionDescriptor.DisplayName);
                context.HandlerInstance.
                logger.LogWarning($"{context.HandlerInstance.GetType().ToString()} selected.");
            }
        }
    }
    public class PositiveParameterAttribute : Attribute, IPageFilter
    {
        private readonly string _parameterName;

        public PositiveParameterAttribute(string parameterName)
        {
            _parameterName = parameterName;
        }

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        {
        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (!context.HandlerArguments.Keys.Contains(_parameterName))
            {
                context.Result = new BadRequestObjectResult($"Parameter {_parameterName} missing.");
            }
            var value = context.HandlerArguments[_parameterName].ToString();
            if (int.TryParse(value, out var result))
            {
                if (result < 0)
                {
                    context.Result = new BadRequestObjectResult($"Parameter {_parameterName} is negative.");
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult($"Parameter {_parameterName} was not a valid integer.");
            }
        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {
        }
    }
}
