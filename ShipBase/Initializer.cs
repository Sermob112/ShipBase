
using Microsoft.Extensions.DependencyInjection;
using ShipBase.DAL.Interfaces;
using ShipBase.DAL.Repositories;
using ShipBase.Domain.Entity;
using ShipBase.Service.Implementations;
using ShipBase.Service.Interfaces;

namespace ShipBase
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {

            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();

        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAccountService, AccountService>();
        }
    }
}