using LearnHub_Api.Contracts.Users;

namespace LearnHub_Api.Services
{
    public interface IUserService
    {
        Task<Result> ChangePasswordAsync(string Id, ChangePasswordRequest request);
        Task<Result<UsersProfileResponse>> GetProfileAsync(String Id);
        Task<Result> UpdateUserProfileAsync(string Id, UpdateProfileRequest request);
    }
}
