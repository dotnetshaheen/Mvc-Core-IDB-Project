using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using mvc_asp_core.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using mvc_asp_core.Models.UserIdentityModel;
using mvc_asp_core.Models;
using Microsoft.AspNetCore.Session;
namespace mvc_asp_core
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ESAD")));

            services.AddDbContext<HospitalContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ESAD")));

            //========= Session =====================
            services.AddDistributedMemoryCache();
            services.AddSession();
            //services.AddDefaultIdentity<IdentityModel>()
            //    .AddRoles<IdentityRole>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentity<IdentityModel, IdentityRole>()
            .AddRoleManager<RoleManager<IdentityRole>>()
            .AddDefaultUI()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAuthorization(op =>
            //    op.AddPolicy("userOp",
            //    policy => policy.RequireClaim("Update")
            //                   .RequireClaim("Delete")));
            //services.AddAuthorization(op =>
            // op.AddPolicy("userRole",
            // policy => policy.RequireRole("Admin")));



            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseSession();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
