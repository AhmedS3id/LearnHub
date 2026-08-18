namespace LearnHub_Api.Services
{
    public interface IRefreshTokenCleanupJob
    {
        Task CleanupAsync();
    }
}
