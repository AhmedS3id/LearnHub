using LearnHub_Api.Contracts.Users;
using LearnHub_Api.Errors;

namespace LearnHub_Api.Services
{
    public class UserService(UserManager<ApplicationUser>userManager,ApplicationDbContext context ):IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager = userManager;
        private readonly ApplicationDbContext _context = context;
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
    }
}
