using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OdeToFood.Contracts;
using OdeToFood.Entities;
using OdeToFood.Services;

namespace OdeToFood
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
            // New instance for new request
            services.AddScoped<IRestaurantData, SqlRestaurantData>();
            // Entity Framework
            services.AddDbContext<OdeToFoodDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("OdeToFood"))
                );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IGreeter greeter)
        {
            // Register Logger
            loggerFactory.AddConsole();

            // Handle Errors according to Development/Production/Staging Environment
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(new ExceptionHandlerOptions
                {
                    ExceptionHandler = context => context.Response.WriteAsync("Opps!")
                });
            }

            app.UseFileServer();

            // Use ASP.NET MVC Framework
            app.UseMvc(ConfigureRoutes);

            // If none of the routes match
            app.Run(ctx => ctx.Response.WriteAsync("Page not Found..."));
        }

        // To configure Routes for MVC framework
        // You can define any number of routes as needed
        // This is called convention bases routing.
        private void ConfigureRoutes(IRouteBuilder routebuilder)
        {
            // route for path /Home/Index
            routebuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}