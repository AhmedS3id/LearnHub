using LearnHub_Api.Abstractions.Consts;
using LearnHub_Api.Contracts.User;

namespace LearnHub_Api.Services
{
    public class UserServices(UserManager<ApplicationUser> userManager, ApplicationDbContext context) : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;

        public async Task<IEnumerable<UserResponse>> GetAllAsync() =>
            await (from u in _context.Users
                   join ur in _context.UserRoles
                   on u.Id equals ur.UserId
                   join r in _context.Roles
                   on ur.RoleId equals r.Id
                   group r by new
                   {
                       u.Id,
                       u.FirstName,
                       u.LastName,
                       u.Email,
                       u.IsDisabled
                   } into g
                   where !g.Any(x => x.Name == DefaultRoles.Member)
                   select new UserResponse
                   (
                        g.Key.Id,
                        g.Key.FirstName,
                        g.Key.LastName,
                        g.Key.Email!,
                        g.Key.IsDisabled,
                        g.Select(x => x.Name!).ToList()
                   )).ToListAsync();

        public async Task<Result<UserResponse>> GetByIdAsync(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);
            var userRoles = await _userManager.GetRolesAsync(user);
            var Response = new UserResponse
            (
                 id,
                 user.FirstName,
                 user.LastName,
                 user.Email!,
                 user.IsDisabled,
                 userRoles
            );
            return Result.Success(Response);
        }
        public async Task <Result> ChangePasswordAsync(string UserId,ChangePasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(UserId);

            if (user is null)
                return Result.Failure(UserErrors.UserNotFound);

            var result = await _userManager.ChangePasswordAsync(user!, request.CurrentPassword,request.NewPassword);

            foreach (var token in user.RefreshTokens.Where(x => x.IsActive))
            {
                token.RevokedOn = DateTime.UtcNow;
            }

            await _userManager.UpdateAsync(user);

            if (result.Succeeded) 
                return Result.Success();

            var error=result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result<UsersProfileResponse>> GetProfileAsync(String Id)
        {

            var user = await _userManager.Users
                .Where(x => x.Id == Id)
                .ProjectToType<UsersProfileResponse>()
                .FirstAsync();

            return Result.Success(user);
        }

        public async Task<Result> UpdateAsync(string id, UpdateUserRequest request)
        {
            var isEmailExist = await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id != id);
            if (isEmailExist)
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);

            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure<UserResponse>(UserErrors.UserNotFound);

            user = request.Adapt(user);
            user.UserName = request.Email;
            user.NormalizedUserName = request.Email.ToUpper();

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.FirstOrDefault();
            return Result.Failure(new Error(error!.Code, error.Description, StatusCodes.Status400BadRequest));
        }

        public async Task<Result> ChangeRoleAsync(string userId, ChangeRoleRequest request,CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                return Result.Failure(UserErrors.UserNotFound);

            if (request.Role != DefaultRoles.Member &&
                request.Role != DefaultRoles.Instructor &&
                request.Role != DefaultRoles.Admin)
            {
                return Result.Failure(UserErrors.RoleNotFound);
            }

            if (await _userManager.IsInRoleAsync(user, request.Role))
                return Result.Failure(UserErrors.AlreadyInRole);

            await using var transaction =
                await _context.Database.BeginTransactionAsync(
                    cancellationToken);

            try
            {
                var currentRoles = await _userManager.GetRolesAsync(user);

                if (currentRoles.Any())
                {
                    var removeResult =
                        await _userManager.RemoveFromRolesAsync(
                            user,
                            currentRoles);

                    if (!removeResult.Succeeded)
                    {
                        await transaction.RollbackAsync(cancellationToken);
                        return Result.Failure(
                            UserErrors.RoleAssignmentFailed);
                    }
                }

                var addResult =await _userManager.AddToRoleAsync(user, request.Role);

                if (!addResult.Succeeded)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    return Result.Failure(
                        UserErrors.RoleAssignmentFailed);
                }

                await transaction.CommitAsync(cancellationToken);

                return Result.Success();
            }
            catch
            {
                await transaction.RollbackAsync(cancellationToken);
                throw;
            }
        }

        public async Task<Result> UpdateUserProfileAsync(string Id, UpdateProfileRequest request)
        {
            var user = await _userManager.Users
                .Where(x => x.Id == Id)
                .ExecuteUpdateAsync(s => s
                     .SetProperty(u => u.FirstName, request.FirstName)
                      .SetProperty(u => u.LastName, request.LastName));
            //user = request.Adapt(user);
            //await _userManager.UpdateAsync(user!);

            return Result.Success();
        }

        public async Task<Result> ToggleStatus(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            user.IsDisabled = !user.IsDisabled;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
        public async Task<Result> UnlockAcc(string id)
        {
            if (await _userManager.FindByIdAsync(id) is not { } user)
                return Result.Failure(UserErrors.UserNotFound);

            var result = await _userManager.SetLockoutEndDateAsync(user, null);
            await _userManager.ResetAccessFailedCountAsync(user);

            if (result.Succeeded)
                return Result.Success();

            var error = result.Errors.First();

            return Result.Failure(new Error(error.Code, error.Description, StatusCodes.Status400BadRequest));
        }
    }
}
