using AeroTripProject.Application.Repostories;
using AeroTripProject.Domain.Entities.Identity;
using AeroTripProject.Persistence.Context;
using AeroTripProject.Persistence.Repostories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AeroTripProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AeroTripDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AeroTripDbContext>()
                .AddDefaultTokenProviders();

            services.AddScoped(typeof(IRepostory<>), typeof(Repostory<>));
        }
    }
}