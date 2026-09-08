using LearnHub_Api.Abstractions;
using LearnHub_Api.Common;
using LearnHub_Api.Contracts.User;

namespace LearnHub_Api.Services
{
    public interface IUserService
    {
        Task<Result> ChangePasswordAsync(string Id, ChangePasswordRequest request);
        Task<Result<UsersProfileResponse>> GetProfileAsync(String Id);
        Task<Result> UpdateUserProfileAsync(string Id, UpdateProfileRequest request);
        Task<Result> UpdateAsync(string id, UpdateUserRequest request);
        Task<Result> ToggleStatus(string id);
        Task<Result> UnlockAcc(string id);
        Task<Result> ChangeRoleAsync(string userId, ChangeRoleRequest role,CancellationToken cancellationToken);
        Task<Result<UserResponse>> GetByIdAsync(string id);
        Task<Result<PaginatedList<UserResponse>>> GetAllAsync(RequestFilter filter, CancellationToken cancellationToken);

    }
}
