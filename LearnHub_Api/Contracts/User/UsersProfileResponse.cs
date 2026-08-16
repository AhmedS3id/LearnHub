namespace LearnHub_Api.Contracts.User
{
    public record UsersProfileResponse(
        string Email,
        string UserName,
        string FirstName,
        string LastName
        );
}
