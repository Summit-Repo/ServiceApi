using RepositoryLayer.IRepository;
using Microsoft.Extensions.DependencyInjection;
using RepositoryLayer.Repository;

namespace RepositoryLayer.Dependency
{
    public static class RegisterDependency
    {
        public static void GetRepositoryDependency(this IServiceCollection services)
        {
            //UnitofWork
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<ITestRepository, TestRpository>();

            services.AddScoped<IErrorDetailRepository, ErrorDetailRepository>();

            services.AddScoped<IUsersRepository, UsersRepository>();

            services.AddScoped<IRoleRepository, RoleRepository>();

            services.AddScoped<IUserRolesRepository, UserRoleRepository>();

            services.AddScoped<IUserClaimsRepository, UserClaimsRepository>();

            
        }

    }
}
