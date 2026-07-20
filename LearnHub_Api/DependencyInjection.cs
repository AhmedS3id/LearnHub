using LearnHub_Api.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LearnHub_Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("ConnectionString'DefaultConnection' Not found");

            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(ConnectionString));

            return services;
        }
    }
}
