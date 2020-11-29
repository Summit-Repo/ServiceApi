using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLayer;
using BusinessLayer.Business;
using BusinessLayer.IBusiness;
using RepositoryLayer.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RepositoryLayer;
using RepositoryLayer.Dependency;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Service.Utility;
using Microsoft.IdentityModel.Logging;
using ModelLayer.ViewModel;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace Service
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
            services.AddControllers();
            IServiceCollection serviceCollection = services.AddDbContext<Context>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //JWT Authentication START
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = Configuration.GetSection("Jwt").GetSection("Issuer").Value,
                     ValidAudience = Configuration.GetSection("Jwt").GetSection("Audience").Value,
                     //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt: SecretKey"])),
                     IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Jwt").GetSection("SecretKey").Value)),
                     ClockSkew = TimeSpan.Zero
                 };


             });

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            IdentityModelEventSource.ShowPII = true;
            //JWT Authentication END

            //IOption
            services.Configure<Utility.HashingOptions>(Configuration.GetSection("HashingOptions"));
            services.Configure<JwtDto>(Configuration.GetSection("Jwt"));

            //Swagger
            services.AddSwaggerDocument();

            //Repository
            services.GetRepositoryDependency();

            //Business DI
            services.GetBusinessDependency();


            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();   //https://localhost:44319/swagger/index.html
        }
    }
}
