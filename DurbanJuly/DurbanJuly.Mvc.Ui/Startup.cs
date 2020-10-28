using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DurbanJuly.Common.Configuration;
using DurbanJuly.Domain;
using DurbanJuly.Infrastructure.Data.Contexts;
using DurbanJuly.Mvc.Ui.Validation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DurbanJuly.Mvc.Ui
{
    public class Startup
    {
        protected static readonly string DatabaseConfigurationName = "DatabaseConfiguration";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<DatabaseConfiguration>(Configuration.GetSection(DatabaseConfigurationName));
            var connString = Configuration.GetSection(DatabaseConfigurationName)["ConnectionString"];

            services.AddIdentity<User, IdentityRole>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<DefaultDbContext>();

            services.AddMvc()
         .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<EventValidator>());

            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<DefaultDbContext>(cfg =>
   
           cfg.UseSqlServer(connString));
           //cfg.UseInMemoryDatabase("HollywoodTest"));
            services.AddRazorPages();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Register your own things directly with Autofac here. Don't
            // call builder.Populate(), that happens in AutofacServiceProviderFactory
            // for you.


            builder.RegisterAssemblyTypes(Assembly.Load("DurbanJuly.Domain.Services"))
                           .Where(t => t.Name.EndsWith("Service", StringComparison.OrdinalIgnoreCase))
                           .PropertiesAutowired();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
