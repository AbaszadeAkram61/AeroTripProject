using AeroTripProject.Application.Dtos;
using AeroTripProject.Application.Repostories;
using AeroTripProject.Application.Validations;
using AeroTripProject.Domain.Entities.Identity;
using AeroTripProject.Persistence.Context;
using AeroTripProject.Persistence.Repostories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace AeroTripProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceRegistration(this IServiceCollection services)
        {
            services.AddDbContext<AeroTripDbContext>(option =>
            {
                option.UseSqlServer("Server=ABASOV-194\\SQLEKREM;Database=AeroTripProjectDb;User Id=sa;Password=edik12;Trusted_Connection=True;TrustServerCertificate=True;");
            });

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AeroTripDbContext>()
               .AddErrorDescriber<CustomIdentityValidator>();

            services.AddScoped(typeof(IRepostory<>), typeof(Repostory<>));
        }
    }
}
