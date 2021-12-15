using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JDMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using ReflectionIT.Mvc.Paging;

namespace JDMovie
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
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.IsEssential = true;
            });
            var stringConnectdb = Configuration.GetConnectionString("dbJDMovie");
            services.AddDbContext<dbDACNContext>(options => options.UseSqlServer(stringConnectdb));
            //services.AddDbContext<dbDACNContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("dbJDMovie")));
            services.AddSession();
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
           
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMemoryCache();

#pragma warning disable CS0618 // Type or member is obsolete
            services.AddPaging();
#pragma warning restore CS0618 // Type or member is obsolete

            services.AddNotyf(config => { config.DurationInSeconds = 3; config.IsDismissable = true; config.Position = NotyfPosition.BottomRight; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
       
            app.UseRouting();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
