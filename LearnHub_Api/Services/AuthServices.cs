
namespace LearnHub_Api.Services
{
    public class AuthServices(UserManager<ApplicationUser> UserManager) : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _UserManager = UserManager;

        public async Task<Result> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
           var existingUser= await _UserManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return Result.Failure(UserCredentials.DuplicatedEmaiil);
            var user = request.Adapt<ApplicationUser>();
            var result = await _UserManager.CreateAsync(user, request.Password);
            if(result.Succeeded)
            {
                return Result.Success();
            }
            var error = result.Errors.First();
            return Result.Failure(new Error(error.Code, error.Description,StatusCodes.Status400BadRequest));
        }
    }


}
