using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoveThemBackWebApp.Data;
using LoveThemBackWebApp.Models.Interfaces;
using LoveThemBackWebApp.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace LoveThemBackWebApp
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public Startup(IConfiguration configuration)
        {
            var builder = new ConfigurationBuilder().AddEnvironmentVariables();
            builder.AddUserSecrets<Startup>();
            Configuration = builder.Build();


        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddMvc();

            services.AddDbContext<LTBDBContext>(options =>

            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<IFavorites, FavoriteService>();
            services.AddTransient<IProfiles, ProfileService>();
            services.AddTransient<IReviews, ReviewsService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseSession();
            app.UseMvc(routes =>
           {
               routes.MapRoute(
                   name: "default",
                   template: "{controller=Login}/{action=Index}/{id?}");
           });

            app.UseStaticFiles();
            app.UseSession();

        }
    }
}