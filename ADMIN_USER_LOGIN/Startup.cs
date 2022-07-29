using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADMIN_USER_LOGIN.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ADMIN_USER_LOGIN
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
         
            services.AddDbContext<AppDbContext>(item =>
            item.UseSqlServer(Configuration.GetConnectionString("MyConStr")));

            services.AddControllers();

            services.AddCors(setup =>
            {
                setup.AddPolicy("default", options =>
                {
                    options.AllowAnyHeader().AllowAnyOrigin().AllowAnyOrigin().AllowAnyMethod();
                });

            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
