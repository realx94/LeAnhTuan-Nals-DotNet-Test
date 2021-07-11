using System.Text;
using Authentication.Extension;
using Core.Settings;
using Core.ViewModels.Profiles;
using Domain.Services;
using Domain.Validators;
using FluentValidation.AspNetCore;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using VueCliMiddleware;

namespace Authentication
{
    public class Startup
    {
        private const string clientAppDev = "ClientApp";
        private const string clientAppProd = "dist";
        private const string clientVueCli = "serve";
        private const string PrimaryConfigSection = "AppSettings";
        private AppSettings Settings { get; set; }
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddFluentValidation(fv =>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<ValidatorWriteup>();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDbContext<AuthDbContext>(opt => opt.UseInMemoryDatabase("Authentication"));

            services.AddCors();

            services.AddControllers();

            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = clientAppDev;
            });

            //Config Setting
            Settings = services.AddAppSetting<AppSettings>(Configuration, PrimaryConfigSection);

            //Config Repositories
            services.UseRepositories();

            //Config Services
            services.UseServices();

            //Add AutoMapper
            services.AddAutoMapper(typeof(ProfileWriteup).Assembly);

            //Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ValidIssuer = this.Settings.Auth.Authority,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = false,
                    RequireSignedTokens = false
                };

                if (!string.IsNullOrEmpty(this.Settings.Auth.SecretKey))
                {
                    options.TokenValidationParameters.RequireSignedTokens = true;
                    options.TokenValidationParameters.ValidateIssuerSigningKey = true;
                    options.TokenValidationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Settings.Auth.SecretKey));
                }

                if (!string.IsNullOrEmpty(this.Settings.Auth.Audience))
                {
                    options.TokenValidationParameters.ValidateAudience = true;
                    options.TokenValidationParameters.ValidAudience = this.Settings.Auth.Audience;
                }

                if (!string.IsNullOrEmpty(this.Settings.Auth.Authority))
                {
                    options.TokenValidationParameters.ValidateIssuer = true;
                    options.TokenValidationParameters.ValidIssuer = this.Settings.Auth.Authority;
                }
            });

            //Config API
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Config Exception
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/500");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRewriter(new RewriteOptions().Add(new RedirectLowerCaseRule()));
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles();

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
