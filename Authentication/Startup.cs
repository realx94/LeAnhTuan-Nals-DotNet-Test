using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VueCliMiddleware;

namespace Authentication
{
    public class Startup
    {
        public const string clientAppDev = "ClientApp";
        public const string clientAppProd = "dist";
        public const string clientVueCli = "serve";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<AuthDbContext>(opt => opt.UseInMemoryDatabase("Authentication"));

            services.AddCors();

            services.AddControllers();

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = clientAppDev;
            });
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
                app.UseHsts();
            }
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseEndpoints(x => x.MapControllers());

            app.UseSpaStaticFiles();

            app.UseSpa(spa =>
            {
                if (env.IsDevelopment())
                {
                    spa.Options.SourcePath = clientAppDev;
                    spa.UseVueCli(npmScript: clientVueCli);
                }
                else
                    spa.Options.SourcePath = clientAppProd;
            });
        }
    }
}
