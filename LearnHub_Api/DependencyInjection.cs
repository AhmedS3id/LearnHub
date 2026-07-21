
namespace LearnHub_Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("ConnectionString'DefaultConnection' Not found");

            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(ConnectionString));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddScoped<IAuthServices, AuthServices>();

            services.AddMapsterServicesConfig();

            return services;
        }

        private static IServiceCollection AddMapsterServicesConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }
    }
}
