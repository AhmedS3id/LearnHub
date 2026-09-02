using LearnHub_Api.Abstractions.Consts;
using LearnHub_Api.Contracts.User;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("users")]
public class UserManagementController(
    IUserService userService) : ControllerBase
{
    private readonly IUserService _userService = userService;

    [HttpPut("{userId}/role")]
    [Authorize(Roles = DefaultRoles.Admin)]
    public async Task<IActionResult> ChangeRole([FromRoute] string userId, [FromBody] ChangeRoleRequest Role, CancellationToken cancellationToken)
    {
        var result = await _userService.ChangeRoleAsync(userId, Role, cancellationToken);

        return result.IsSuccess ? NoContent() : result.ToProblem();
    }
}