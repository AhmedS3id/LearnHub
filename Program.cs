using Hangfire;
using HealthChecks.UI.Client;
using LearnHub_Api;
using LearnHub_Api.Authentication.Filter;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configurations) =>
{
    configurations.ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
// Order matters: exception handling wraps everything, then https/cors/authn/authz,
// then rate limiting, and only then endpoint execution (MapControllers).

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseSerilogRequestLogging();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    DashboardTitle = "LearnHub Dashboard",
    Authorization = [new HangfireDashboardAuthorizationFilter()]
});

RecurringJob.AddOrUpdate<IRefreshTokenCleanupJob>(
    "cleanup-expired-refresh-tokens",
    job => job.CleanupAsync(),
    Cron.Daily(3, 0));

app.MapControllers();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
