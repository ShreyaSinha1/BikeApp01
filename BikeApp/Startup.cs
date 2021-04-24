using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeApp.AppContext;
//using Fluent.Infrastructure.FluentModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BikeApp
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
            services.AddAutoMapper(typeof(MappingProfile));
           
            services.AddDbContext<VroomDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddScope<IDbIntializer, DbIntializer>();
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<VroomDbContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();
            services.AddCloudscribePagination();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IDbIntializer dbIntializer)
        {
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
            dbIntializer.Initilize();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "ByYearMonth",
                   pattern: "make/bikes/{year}/{month:int}",
                   new { controller="make",action="ByYearMonth"});
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
