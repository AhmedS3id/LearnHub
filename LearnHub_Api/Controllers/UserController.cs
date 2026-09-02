using LearnHub_Api.Authentication.Filter;
using LearnHub_Api.Contracts.User;
using LearnHub_Api.Extensions;
using LearnHub_API.Abstractions.Consts;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [Route("me")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("")]
        public async Task<IActionResult> GetUserProfile()
        {
            var result = await _userService.GetProfileAsync(User.GetUserId()!);

            return result.IsSuccess ? Ok(result.Value): result.ToProblem();

        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
        {
            var result = await _userService.ChangePasswordAsync(User.GetUserId()!, request);

            return result.IsSuccess ? NoContent() : result.ToProblem();

        }

        [HttpPut("info")]
        [HasPermission(Permissions.UpdateProfile)]
        public async Task<IActionResult> UpdateUserProfile( [FromBody] UpdateProfileRequest request)
        {
            var result = await _userService.UpdateUserProfileAsync(User.GetUserId()!, request);
            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}
