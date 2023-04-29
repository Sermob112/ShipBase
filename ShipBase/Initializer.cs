
using Microsoft.Extensions.DependencyInjection;
using ShipBase.DAL.SectionOne.Interfaces;
using ShipBase.DAL.SectionOne.Repositories;
using ShipBase.Domain.SectionOne.Entity;
using ShipBase.Service.SectionOne.Implementations;
using ShipBase.Service.SectionOne.Interfaces;

namespace ShipBase
{
    public static class Initializer
    {
        public static void InitializeRepositories(this IServiceCollection services)
        {

            services.AddScoped<IBaseRepository<User>, UserRepository>();
            services.AddScoped<IBaseRepository<Profile>, ProfileRepository>();
            services.AddScoped<IBaseRepository<Customer>, CustomerRepository>();
            services.AddScoped<IBaseRepository<PurchasingData>, PurchasingDataRepository>();
        }

        public static void InitializeServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IPurchasingDataService, PurchasingDataService>();
        }
    }
}