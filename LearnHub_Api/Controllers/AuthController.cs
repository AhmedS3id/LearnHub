using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IAuthServices authServices) : ControllerBase
    {

            private readonly IAuthServices _authServices = authServices;

        [HttpPost("register")]
        public async Task<IActionResult> Register(
           [FromBody] RegisterRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authServices.RegisterAsync(request, cancellationToken);

            return result.IsSuccess
                ? Ok(new
                {
                    Message = "Registration completed successfully. Please check your email to confirm your account."
                })
                : result.ToProblem();
        }

        [HttpPost("")]
        public async Task<IActionResult> Login(
           [FromBody] LoginRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authServices.LoginAsync(request, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(
           [FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authServices.GetRefreshTokenAsync(request.Token,request.RefreshToken, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : result.ToProblem();
        }
        [HttpPost("logout")]
        public async Task<IActionResult> RevokeRefreshToken(
           [FromBody] RefreshTokenRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _authServices.RevokeRefreshTokenAsync(request.Token,request.RefreshToken, cancellationToken);

            return result.IsSuccess
                ? Ok()
                : result.ToProblem();
        }

        [HttpPost("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(
            [FromBody] ConfirmEmailRequest request)
        {
            var result = await _authServices.ConfirmationEmail(request);

            return result.IsSuccess
                ? Ok()
                : result.ToProblem();
        }

        [HttpPost("resend-email-confirm")]
        public async Task<IActionResult> ResendEmailConfirmation([FromBody] ResendConfirmationEmailRequest request)
        {
            var Result = await _authServices.ResendConfirmationEmail(request);

            return Result.IsSuccess ? Ok() : Result.ToProblem();
        }
        [HttpPost("forget-password")]
        public async Task<IActionResult> ForgetPassword([FromBody]ForgetPasswordRequest email)
        {
            var Result = await _authServices.ForgetPasswordAsync(email);

            return Result.IsSuccess ? Ok() : Result.ToProblem();
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            var Result = await _authServices.ResetPasswordAsync(request);

            return Result.IsSuccess ? Ok() : Result.ToProblem();
        }

    }
}
