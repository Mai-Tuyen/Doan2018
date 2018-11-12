using DigitalShop.Service;
using DigitalShop.Service.IRepository;
using DigitialShop.Service.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitalShop
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
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            //services.AddDbContext<DigitalDBContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            var connectionString = Configuration["connectionStrings:DigitalShopConnectionString"];
            services.AddDbContext<DigitalDBContext>(x => x.UseSqlServer(connectionString));
            services.AddMvc();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                //routes.MapRoute(
                //    name: "default",
                //    template: "{controller=Home}/{action=Index}/{id?}");

                //routes.MapAreaRoute(
                //    name: "adminHome",
                //    areaName: "Admin",routes.MapRoute("areaRoute", "{area:exists}/{controller=Admin}/{action=Index}/{id?}");
                //    template: "{controller=Home}/{action=Index}/admin/{id?}");
                routes.MapRoute("Admin","{area:exists}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
