namespace LearnHub_Api.Services
{
    public interface IAuthServices
    {
        Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<Result> ConfirmationEmail(ConfirmEmailRequest request);
        Task<Result> ResendConfirmationEmail(ResendConfirmationEmailRequest request);

    }
}
