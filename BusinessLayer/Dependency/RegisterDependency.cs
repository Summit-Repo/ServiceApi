using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using RepositoryLayer.IRepository;
using RepositoryLayer.Repository;
using BusinessLayer.IBusiness;
using System;

namespace BusinessLayer.Business
{
    public static class RegisterDependency
    {
        public static void GetBusinessDependency(this IServiceCollection services)
        {
            // var mapperConfig = new MapperConfiguration(x=> x.AddProfile(new Mapper()));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddScoped<ITestOpertaions, TestOperations>();

            services.AddScoped<ILoginAccess, LoginAccess>();

            
        }

    }
}
