
using Microsoft.EntityFrameworkCore;

namespace LearnHub_Api.Services
{
    public class RefreshTokenCleanupJob(ApplicationDbContext context,ILogger<RefreshTokenCleanupJob> logger) : IRefreshTokenCleanupJob
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<RefreshTokenCleanupJob> _logger = logger;

        public async Task CleanupAsync()
        {
            var revokedCutoffDate = DateTime.UtcNow.AddDays(-7);

            try
            {
            var deletedCount = await _context.Database.ExecuteSqlInterpolatedAsync(
            $@"DELETE FROM RefreshTokens 
               WHERE ExpireOn < {DateTime.UtcNow} 
                  OR (RevokedOn IS NOT NULL AND RevokedOn < {revokedCutoffDate})");
                //    await _context.Users
                //.Include(u => u.RefreshTokens.Where(t =>
                //    t.ExpireOn < DateTime.UtcNow ||
                //    (t.RevokedOn != null && t.RevokedOn < revokedCutoffDate)))
                //.Where(u => u.RefreshTokens.Any(t =>
                //    t.ExpireOn < DateTime.UtcNow ||
                //    (t.RevokedOn != null && t.RevokedOn < revokedCutoffDate)))
                //.ToListAsync();

                //var deletedCount = 0;

                //foreach (var user in usersWithOldTokens)
                //{
                //    deletedCount += user.RefreshTokens.Count;
                //    user.RefreshTokens.Clear(); 
                //}

                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Refresh token cleanup completed. Deleted {Count} tokens at {Time}",
                    deletedCount, DateTime.UtcNow);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while cleaning up refresh tokens");
                throw;
            }
        }
    }
}
