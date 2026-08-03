using FluentValidation.AspNetCore;
using LearnHub_Api.Settings;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace LearnHub_Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("ConnectionString'DefaultConnection' Not found");

            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(ConnectionString));
            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IAuthServices, AuthServices>();
            services.AddScoped<IEmailSender, EmailServices>();
            

            services.AddMapsterServicesConfig();
            services.AddAuthConfig();

            return services;
        }

        private static IServiceCollection AddMapsterServicesConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }
        private static IServiceCollection AddAuthConfig(this IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            return services;
        }
    }
}
