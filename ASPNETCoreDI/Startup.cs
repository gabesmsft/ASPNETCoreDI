using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ASPNETCoreDI
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            //here we are injecting the configuration providers that were registered in Program.cs.
            // If you don't explicitly register configuration providers in Progam.cs, you can still use the below line and 
            //it will use the defaults, which are  appsettings.json (for Production) and appsettings.Development.json (for Development).
            //coincidentally we are choosing to use these same providers anyway

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Here we are registering services for DI
        public void ConfigureServices(IServiceCollection services)
        {
            //this services is provided by the framework
            services.AddControllersWithViews();

            //Registering MyDependency as a service so that it can be dependency-injected into a client
            services.AddTransient<IMyDependency1, MyDependency1>(s => new MyDependency1("MyDependency1 successfully injected"));

            //we won't actually inject MyDependency2, for the sake of brevity. But am including this example of how to register the service 
            services.AddTransient<IMyDependency2, MyDependency2>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        //webhost middleware goes here:
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation("In the Startup Configure method at " + DateTime.UtcNow);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
