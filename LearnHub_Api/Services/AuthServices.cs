
using LearnHub_Api.Authentication;
using LearnHub_Api.Consts;
using LearnHub_Api.Entities;
using LearnHub_Api.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Security.Cryptography;
using System.Text;

namespace LearnHub_Api.Services
{
    public class AuthServices(UserManager<ApplicationUser> UserManager,
        ILogger<AuthServices>logger,
        IJwtProvider jwtProvider,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor) : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _UserManager = UserManager;
        private readonly ILogger<AuthServices> _logger = logger;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _UserManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Result.Failure(UserCredentials.DuplicatedEmaiil);
            var user = request.Adapt<ApplicationUser>();
            var result = await _UserManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var code = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                _logger.LogInformation("Confirmation code : {code}", code);

                await SendConfirmationEmail(user, code);
                return Result.Success();
            }
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            if (await _UserManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Failure<AuthResponse>(UserCredentials.InvalidCredentials);

            if (!user.EmailConfirmed)
                return Result.Failure<AuthResponse>(UserCredentials.EmailNotConfirmed);

            if (!await _UserManager.CheckPasswordAsync(user, request.Password))
                return Result.Failure<AuthResponse>(UserCredentials.InvalidCredentials);

            var (token, expireIn) = _jwtProvider.GenerateToken(user);
            var refreshToken = GenerateRefreshToken();
            var RFExpirationDate = DateTime.UtcNow.AddDays(15);

            foreach (var tokens in user.RefreshTokens.Where(t => t.IsActive))
            {
                tokens.RevokedOn = DateTime.UtcNow;
            }

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpireOn = RFExpirationDate
            });
            await _UserManager.UpdateAsync(user);
            var response = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, token, expireIn,refreshToken,RFExpirationDate);
                return Result.Success(response);
        }
        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken)
        {
            var userId = _jwtProvider.ValidateToken(Token);
            if (userId is null)
                return Result.Failure<AuthResponse>(UserCredentials.InvalidJwtToken);

            var user = await _UserManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure<AuthResponse>(UserCredentials.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens
                .SingleOrDefault(x => x.Token == RefreshToken && x.IsActive);

            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserCredentials.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var (newToken, expireIn) = _jwtProvider.GenerateToken(user);
            var newRefreshToken = GenerateRefreshToken();
            var expirationDate = DateTime.UtcNow.AddDays(15);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpireOn = expirationDate
            });
            await _UserManager.UpdateAsync(user);
            var result = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expireIn, newRefreshToken, expirationDate);
            return Result.Success(result);

        }
        public async Task<Result> ConfirmationEmail(ConfirmEmailRequest request)
        {
            if (await _UserManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserCredentials.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserCredentials.DuplicatedConfirmed);

            var code = request.Code;

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            }
            catch (FormatException)
            {
                return Result.Failure(UserCredentials.InvalidCode);
            }

            var result = await _UserManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                return Result.Success();
            }
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

        }
        public async Task<Result> ResendConfirmationEmail(ResendConfirmationEmailRequest request)
        {

            if (await _UserManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Failure(UserCredentials.InvalidCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserCredentials.DuplicatedConfirmed);

            var code = await _UserManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            _logger.LogInformation("Confirmation code : {code}", code);

            await SendConfirmationEmail(user, code);

            return Result.Success();

        }

        private static string GenerateRefreshToken()
        {
             return Convert.ToBase64String (RandomNumberGenerator.GetBytes(64));
        }

        private async Task SendConfirmationEmail(ApplicationUser user, string code)
        {
            var Origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var EmailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
                {
                    {"{{UserName}}",user.FirstName },
                    {"{{AppName}}" ,"Survey Basket"},
                    {"{{ConfirmationLink}}",$"{Origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
                });
           await _emailSender.SendEmailAsync(user.Email!, "✅ Learn Hub : Email Confirmation", EmailBody);
        }
    }
}
