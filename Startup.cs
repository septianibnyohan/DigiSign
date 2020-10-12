using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using DigiSign.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Http;
using DigiSign.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DigiSign
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
            services.AddControllersWithViews();
            
            services.AddDbContext<docsdevEntities>(opts => {
                opts.UseSqlServer(
                    Configuration["ConnectionStrings:DigiSignConnection"]);
            });
            services.AddScoped<IDigiSignRepository, EFDigiSignRepository>();
            services.AddDistributedMemoryCache();
            services.AddSession(options => {
                options.IdleTimeout = System.TimeSpan.FromSeconds(3600);
            });
            services.AddSingleton<AuthHelper>();
            services.AddHttpContextAccessor();
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles(new StaticFileOptions
            {
                ServeUnknownFileTypes = true,
                DefaultContentType = "image/png"
            });

            app.UseSession();

            app.UseRouting();

            app.UseAuthorization();

            ;
            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context => {
                //    await context.Response.WriteAsync("Hello World!");
                //});
                endpoints.MapDefaultControllerRoute();
            });
            //SeedData.EnsurePopulated(app);
        }
    }
}
