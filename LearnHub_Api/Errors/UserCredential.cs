using LearnHub_Api.Consts;

namespace LearnHub_Api.Errors
{
    public class UserCredential
    {
        public static class UserCredentials
        {
            public static readonly Error InvalidCredentials =
                    new("User.InvalidCredentials", "Invalid email/password", StatusCodes.Status401Unauthorized);

            public static readonly Error DisableUser =
                    new("User.Disable", "User is disable please contact with your administrator", StatusCodes.Status401Unauthorized);

            public static readonly Error UserLockedOut =
            new("User.Locked Out", "User is Locked out please contact with your administrator", StatusCodes.Status401Unauthorized);

            public static readonly Error InvalidJwtToken =
                new("User.InvalidJwtToken", "Invalid Jwt token", StatusCodes.Status401Unauthorized);

            public static readonly Error InvalidRefreshToken =
                new("User.InvalidRefreshToken", "Invalid refresh token", StatusCodes.Status401Unauthorized);

            public static readonly Error DuplicatedEmaiil =
                new("User.InvalidEmail", "Invalid Email ,Email is exist", StatusCodes.Status409Conflict);

            public static readonly Error EmailNotConfirmed =
              new("User.EmailNotConfirmed", "EmailNotConfirmed", StatusCodes.Status401Unauthorized);

            public static readonly Error InvalidCode =
              new("User.InvalidCode", "Invalid Code", StatusCodes.Status401Unauthorized);

            public static readonly Error DuplicatedConfirmed =
              new("User.DuplicatedConfirmed", " Email already confirmed ", StatusCodes.Status401Unauthorized);
            public static readonly Error UserNotFound =
              new("User.UserNotFound", " User Not Found  ", StatusCodes.Status404NotFound);
        }
    }
}
