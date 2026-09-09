using Hangfire;
using HealthChecks.UI.Client;
using LearnHub_Api;
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

app.UseHttpsRedirection();

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    DashboardTitle = "LearnHub Dashboard",
    //IsReadOnlyFunc = (DashboardContext context) => true
});

RecurringJob.AddOrUpdate<IRefreshTokenCleanupJob>(
    "cleanup-expired-refresh-tokens",
    job => job.CleanupAsync(),
    Cron.Daily(3, 0));

app.UseSerilogRequestLogging();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.UseRateLimiter();

app.MapHealthChecks("health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
