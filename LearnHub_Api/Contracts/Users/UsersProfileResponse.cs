namespace LearnHub_Api.Contracts.Users
{
    public record UsersProfileResponse(
        string Email,
        string UserName,
        string FirstName,
        string LastName
        );
}
