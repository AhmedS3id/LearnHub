namespace LearnHub_Api.Services
{
    public interface IAuthServices
    {
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken);
        Task<Result> RevokeRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken);
        Task<Result> ConfirmationEmail(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmail(ResendConfirmationEmailRequest request);
        Task<Result> ForgetPasswordAsync(ForgetPasswordRequest request);

    }
}
