using Hangfire;
using LearnHub_Api.Abstractions.Consts;
using LearnHub_Api.Authentication;
using LearnHub_Api.Helpers;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace LearnHub_Api.Services
{
    public class AuthServices(UserManager<ApplicationUser> UserManager,
        ApplicationDbContext context,
        ILogger<AuthServices>logger,
        IJwtProvider jwtProvider,
        IEmailSender emailSender,
        IHttpContextAccessor httpContextAccessor) : IAuthService
    {
        private readonly UserManager<ApplicationUser> _UserManager = UserManager;
        private readonly ApplicationDbContext _context = context;
        private readonly ILogger<AuthServices> _logger = logger;
        private readonly IJwtProvider _jwtProvider = jwtProvider;
        private readonly IEmailSender _emailSender = emailSender;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingUser = await _UserManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Result.Failure(UserErrors.EmailAlreadyExists);
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
            //if (await _UserManager.FindByEmailAsync(request.Email) is not { } user)
            //    return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var normalizedEmail = _UserManager.NormalizeEmail(request.Email);

            var user = await _context.Users
                .Include(x => x.RefreshTokens.Where(t => t.RevokedOn == null && t.ExpireOn > DateTime.UtcNow))
                .FirstOrDefaultAsync(x => x.NormalizedEmail == normalizedEmail, cancellationToken);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            if (!user.EmailConfirmed)
                return Result.Failure<AuthResponse>(UserErrors.EmailNotConfirmed);

            if (!await _UserManager.CheckPasswordAsync(user, request.Password))
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var (userRoles, Permission) = await GetRolesAndPermission(user, cancellationToken);

            var (token, expireIn) = _jwtProvider.GenerateToken(user, userRoles, Permission);
            var refreshToken = GenerateRefreshToken();
            var refreshTokenExpirationDate = DateTime.UtcNow.AddDays(15);

            foreach (var activeToken in user.RefreshTokens.Where(x => x.IsActive))
            {
                activeToken.RevokedOn = DateTime.UtcNow;
            }

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                ExpireOn = refreshTokenExpirationDate
            });

            await _context.SaveChangesAsync(cancellationToken);

            var response = new AuthResponse(
                user.Id, user.Email, user.FirstName, user.LastName,
                token, expireIn, refreshToken, refreshTokenExpirationDate);

            return Result.Success(response);
        }
        public async Task<Result<AuthResponse>> GetRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken)
        {
            var userId = _jwtProvider.ValidateExpiredToken(Token);
            if (userId is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var user = await _context.Users
                .Include(x => x.RefreshTokens.Where(t => t.RevokedOn == null && t.ExpireOn > DateTime.UtcNow))
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var userRefreshToken = user.RefreshTokens
                .SingleOrDefault(x => x.Token == RefreshToken);

            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            var (userRoles, Permission) = await GetRolesAndPermission(user, cancellationToken);

            var (newToken, expireIn) = _jwtProvider.GenerateToken(user, userRoles, Permission);

            var newRefreshToken = GenerateRefreshToken();
            var expirationDate = DateTime.UtcNow.AddDays(15);

            user.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefreshToken,
                ExpireOn = expirationDate
            });

            await _context.SaveChangesAsync(cancellationToken);

            var result = new AuthResponse(user.Id, user.Email, user.FirstName, user.LastName, newToken, expireIn, newRefreshToken, expirationDate);
            return Result.Success(result);
        }

        public async Task<Result> RevokeRefreshTokenAsync(string Token, string RefreshToken, CancellationToken cancellationToken)
        {
            var user_Id = _jwtProvider.ValidateToken(Token);
            if (user_Id is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidJwtToken);

            var user = await _context.Users
                .Include(x => x.RefreshTokens.Where(t => t.RevokedOn == null && t.ExpireOn > DateTime.UtcNow))
                .FirstOrDefaultAsync(x => x.Id == user_Id, cancellationToken);

            if (user is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidCredentials);

            var userRefreshToken = user.RefreshTokens
                .SingleOrDefault(x => x.Token == RefreshToken);
            if (userRefreshToken is null)
                return Result.Failure<AuthResponse>(UserErrors.InvalidRefreshToken);

            userRefreshToken.RevokedOn = DateTime.UtcNow;

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }

        public async Task<Result> ForgetPasswordAsync(ForgetPasswordRequest request)
        {
            var user = await _UserManager.FindByEmailAsync(request.Email);
            if (user is null)
                return Result.Success();

            if (!user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            var code = await _UserManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            _logger.LogInformation("Confirmation code : {code}", code);

            await SendForgetPasswordEmail(user, code);

            return Result.Success();

        }

        public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await _UserManager.FindByEmailAsync(request.Email);
            if (user is null || !user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailNotConfirmed);

            IdentityResult result;

            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Code));
                result = await _UserManager.ResetPasswordAsync(user, code, request.NewPassword);

            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvalidConfirmationCode);
            }
            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> ConfirmationEmail(ConfirmEmailRequest request)
        {
            if (await _UserManager.FindByIdAsync(request.UserId) is not { } user)
                return Result.Failure(UserErrors.InvalidConfirmationCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailAlreadyConfirmed);

            var code = request.Code;

            try
            {
                code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));

            }
            catch (FormatException)
            {
                return Result.Failure(UserErrors.InvalidConfirmationCode);
            }

            var result = await _UserManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                var roleResult = await _UserManager.AddToRoleAsync(
                    user,
                    DefaultRoles.Member);

                if (!roleResult.Succeeded)
                    return Result.Failure(UserErrors.RoleAssignmentFailed);

                return Result.Success();
            }
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));

        }
        public async Task<Result> ResendConfirmationEmail(ResendConfirmationEmailRequest request)
        {

            if (await _UserManager.FindByEmailAsync(request.Email) is not { } user)
                return Result.Failure(UserErrors.InvalidConfirmationCode);

            if (user.EmailConfirmed)
                return Result.Failure(UserErrors.EmailAlreadyConfirmed);

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

        private async Task SendForgetPasswordEmail(ApplicationUser user, string code)
        {
            var Origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var EmailBody = EmailBodyBuilder.GenerateEmailBody("ForgetPassword", new Dictionary<string, string>
                {
                    {"{{name}}",user.FirstName },
                    { "{{action_url}}", $"{Origin}/auth/forgetPassword?email={user.Email}&code={code}" }
                }
            );
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ LearnHub: Change Password ", EmailBody));
            await Task.CompletedTask;
        }
        private async Task SendConfirmationEmail(ApplicationUser user, string code)
        {
            var Origin = _httpContextAccessor.HttpContext?.Request.Headers.Origin;

            var EmailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
                {
                    {"{{UserName}}",user.FirstName },
                    {"{{AppName}}" ,"LearnHub"},
                    {"{{ConfirmationLink}}",$"{Origin}/auth/emailConfirmation?userId={user.Id}&code={code}" }
                });
            BackgroundJob.Enqueue(() => _emailSender.SendEmailAsync(user.Email!, "✅ LearnHub : Email Confirmation", EmailBody));
            await Task.CompletedTask;
        }
         private async Task <(IEnumerable<string> Roles,IEnumerable<string> Permission)> GetRolesAndPermission(ApplicationUser user,CancellationToken cancellationToken)
        { 
            var userRoles = await _UserManager.GetRolesAsync(user);

            var Permission = await(from r in _context.Roles
                                join p in _context.RoleClaims
                                on r.Id equals p.RoleId
                                where userRoles.Contains(r.Name!)
                                select p.ClaimValue)
                                .Distinct()
                                .ToListAsync(cancellationToken);
            return (userRoles, Permission);
        }
    }
}
