using FluentValidation.AspNetCore;
using Hangfire;
using LearnHub_Api.Authentication;
using LearnHub_Api.Extensions;
using LearnHub_Api.Health;
using LearnHub_Api.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

namespace LearnHub_Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            var ConnectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("ConnectionString'DefaultConnection' Not found");


            var AllowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>()!;

            services.AddCors(options => options.AddDefaultPolicy( builder =>
            builder
            .WithOrigins(AllowedOrigins)
            .AllowAnyMethod()
            .AllowAnyHeader()
            ));

            services.AddDbContext<ApplicationDbContext>(option => option.UseSqlServer(ConnectionString));
            services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation();

            services.AddScoped<IAuthService, AuthServices>();
            services.AddScoped<IEmailSender, EmailServices>();
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<ICategoryService, CategoryServices>();
            services.AddScoped<ICourseService, CourseServices>();
            services.AddScoped<ISectionService, SectionServices>();
            services.AddScoped<ILessonService, LessonServices>();
            services.AddScoped<IEnrollmentService, EnrollmentServices>();
            services.AddScoped<IReviewService, ReviewServices>();
            services.AddScoped<IRefreshTokenCleanupJob, RefreshTokenCleanupJob>();


            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            services.AddHybridCache();

            services.AddMapsterServicesConfig();
            services.AddAuthConfig(configuration);
            services.AddBackgroundJobsConfig(configuration);

            services.AddHealthChecks()
               .AddSqlServer(ConnectionString)
               .AddHangfire(Options => Options.MinimumAvailableServers = 1)
               .AddCheck<MailProviderHealthCheck>(name: "Mail Services");

            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.AddPolicy("ipLimit", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 100,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }));

                options.AddPolicy("userLimit", httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.User.GetUserId() ?? "anonymous",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 200,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        }));

                options.AddConcurrencyLimiter("concurrency", option =>
                {
                    option.PermitLimit = 10;
                    option.QueueLimit = 5;
                    option.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });
            });

            //services.AddApiVersioning(option =>
            //{
            //    option.DefaultApiVersion = new ApiVersion(1, 0);
            //    option.AssumeDefaultVersionWhenUnspecified = true;
            //    option.ReportApiVersions = true;
            //    option.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            //}).AddApiExplorer(option =>
            //{
            //    option.GroupNameFormat = "'v'V";
            //    option.SubstituteApiVersionInUrl = true;
            //});

            return services;
        }

        private static IServiceCollection AddMapsterServicesConfig(this IServiceCollection services)
        {
            var mappingConfig = TypeAdapterConfig.GlobalSettings;
            mappingConfig.Scan(Assembly.GetExecutingAssembly());

            services.AddSingleton<IMapper>(new Mapper(mappingConfig));

            return services;
        }
        private static IServiceCollection AddAuthConfig(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddOptions<JwtOptions>()
           .BindConfiguration("Jwt")
           .ValidateDataAnnotations()
           .ValidateOnStart();

            var JwtSettings = configuration.GetSection("Jwt").Get<JwtOptions>();

            services.AddSingleton<IJwtProvider, JwtProvider>();

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 8;
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            services.AddAuthentication(static option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings?.Key!)),
                    ValidIssuer = JwtSettings?.Issuer,
                    ValidAudience = JwtSettings?.Audience
                };
            });

            return services;
        }
        private static IServiceCollection AddBackgroundJobsConfig(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

            services.AddHangfireServer();

            return services;
        }
    }
}
