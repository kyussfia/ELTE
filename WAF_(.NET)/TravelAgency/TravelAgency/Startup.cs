﻿using ELTE.TravelAgency.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ELTE.TravelAgency
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
			// Dependency injection beállítása az adatbázis kontextushoz
			services.AddDbContext<TravelAgencyContext>(options =>
		        options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

	        // Dependency injection beállítása a Google konfiguráció kollekcióhoz
	        services.Configure<GoogleConfig>(Configuration.GetSection("Google"));

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
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            
            // Adatbázis inicializálása
            DbInitializer.Initialize(app, Configuration.GetValue<string>("ImageStore"));
        }
    }
}
