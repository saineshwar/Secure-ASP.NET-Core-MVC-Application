using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSecurity.EntityStore;
using CoreSecurity.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoreSecurity
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
            services.AddMvc(options =>
            {
                //Registering CustomExceptionFilterAttribute
                options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                //registered Audit Filter
                options.Filters.Add(typeof(AuditFilter));
            });

            // For Setting Session Timeout
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
            });

            services.AddDbContext<DatabaseContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }

            if (env.IsProduction())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseSession();

            #region Cookie
            //app.UseCookiePolicy(new CookiePolicyOptions
            //{
            //    HttpOnly = HttpOnlyPolicy.Always,
            //    Secure = CookieSecurePolicy.Always,
            //    MinimumSameSitePolicy = SameSiteMode.None
            //}); 
            #endregion

            //// X-Content-Type-Options header
            //app.UseXContentTypeOptions();
            //// Referrer-Policy header.
            //app.UseReferrerPolicy(opts => opts.NoReferrer());
            //// X-Xss-Protection header
            //app.UseXXssProtection(options => options.EnabledWithBlockMode());
            //// X-Frame-Options header
            //app.UseXfo(options => options.Deny());
            // Content-Security-Policy header
            //app.UseCsp(opts => opts
            //    //.BlockAllMixedContent()
            //    .StyleSources(s => s.None())
            //    .FontSources(s => s.None())
            //    .FormActions(s => s.Self())
            //    .FrameAncestors(s => s.Self())
            //    .ImageSources(s => s.None())
            //    .ScriptSources(s => s.None())
            //);


            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Index}/{action=Index}/{id?}");
            });
        }
    }
}
