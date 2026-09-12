namespace LearnHub_Api.Contracts.User
{
    public record ChangePasswordRequest(
        string CurrentPassword,
        string NewPassword
        );

    
}
