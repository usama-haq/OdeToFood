﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
            services.AddSingleton(Configuration);
            services.AddSingleton<IGreeter, Greeter>();
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

            // Use index.html for root path if it exists
            //app.UseDefaultFiles();
            // See if the request matches any static files in wwwroot
            //app.UseStaticFiles();

            // Combines functionality of both of the following commands
            // app.UseDefaultFiles();
            // app.UseStaticFiles();
            app.UseFileServer();

            // Use a default welcome page at /welcome route
            app.UseWelcomePage(new WelcomePageOptions
            {
                Path = "/welcome"
            });

            app.Run(async (context) =>
            {
                //   throw new System.Exception("Something went wrong!");

                string pageRoute = "-- Another Link " + context.Request.Scheme.ToString() + "://"
                                    + context.Request.Host.Value.ToString()
                                    + "/welcome";
                string message = greeter.GetGreeting() + pageRoute;
                await context.Response.WriteAsync(message);
            });
        }
    }
}