using LearnHub_Api.Abstractions.Consts;
using LearnHub_Api.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace LearnHub_Api.Controllers
{
    [ApiController]
    [Route("users")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public class UserManagementController(
        IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpPut("{userId}/role")]
        public async Task<IActionResult> ChangeRole([FromRoute] string userId, [FromBody] ChangeRoleRequest Role, CancellationToken cancellationToken)
        {
            var result = await _userService.ChangeRoleAsync(userId, Role, cancellationToken);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
        [Route("")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _userService.GetAllAsync();

            return Ok(result);
        }
        [Route("{userId}")]
        public async Task<IActionResult> GetById([FromRoute]string userId,CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(userId);

            return result.IsSuccess ? NoContent() : result.ToProblem();
        }
    }
}