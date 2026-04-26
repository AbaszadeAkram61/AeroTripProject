using AeroTripProject.Application.Repostories;
using AeroTripProject.Persistence.Context;
using AeroTripProject.Persistence.Repostories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace AeroTripProject.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AeroTripDbContext>(option =>
            {
                option.UseSqlServer("Server=ABASOV-194\\SQLEKREM;Database=AeroTripProjectDb;User Id=sa;Password=edik12;Trusted_Connection=True;TrustServerCertificate=True;");
            });

            services.AddScoped(typeof(IRepostory<>), typeof(Repostory<>));
        }
    }
}
